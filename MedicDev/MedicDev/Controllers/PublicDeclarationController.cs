using MedicDev.Models.Authentication;
using MedicDev.Models.PublicDeclaration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MedicDev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublicDeclarationController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public PublicDeclarationController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("PublicList")]
        [AllowAnonymous]
        public async Task<IActionResult> PublicList()
        {
            var publicDeclarationList = await _db.PublicDeclaration.ToListAsync();
            var list = new List<object>();

            foreach (var publicDeclaration in publicDeclarationList)
            {
                list.Add(new 
                {
                    id = publicDeclaration.Id,
                    title = publicDeclaration.Title,
                    description = publicDeclaration.Description,
                    userId = publicDeclaration.UserId,
                    submittedDate = publicDeclaration.SubmittedDate,
                    declarationDate = publicDeclaration.DeclarationDate,
                    state = publicDeclaration.State
                });
            }

            return new JsonResult(new
            {
                result = list
            });

        }

        [HttpGet]
        [Route("MyList")]
        public async Task<IActionResult> MyList()
        {
            var userId = new Guid(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

            var publicDeclarationList = await _db.PublicDeclaration.Where(x => x.UserId == userId).ToListAsync();
            var list = new List<object>();

            foreach (var publicDeclaration in publicDeclarationList)
            {
                list.Add(new
                {
                    id = publicDeclaration.Id,
                    title = publicDeclaration.Title,
                    description = publicDeclaration.Description,
                    userId = publicDeclaration.UserId,
                    submittedDate = publicDeclaration.SubmittedDate,
                    declarationDate = publicDeclaration.DeclarationDate,
                    state = publicDeclaration.State
                });
            }

            return new JsonResult(new
            {
                result = list
            });

        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var publicDeclaration = await _db.PublicDeclaration.Include(m => m.PublicDeclarationSignatureList).FirstOrDefaultAsync(u => u.Id == id);
            var signatureList = new List<object>();

            if (publicDeclaration == null)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "The Public Declaration does not exist"
                });
            }

            foreach (var signature in publicDeclaration.PublicDeclarationSignatureList)
            {
                signatureList.Add(new { id = signature.Id, personName = signature.PersonName });
            }

            return new JsonResult(new
            {
                id = publicDeclaration.Id,
                title = publicDeclaration.Title,
                description = publicDeclaration.Description,
                userId = publicDeclaration.UserId,
                submittedDate = publicDeclaration.SubmittedDate,
                declarationDate = publicDeclaration.DeclarationDate,
                state = publicDeclaration.State,
                signatures = signatureList 
            });

        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add(PublicDeclarationModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "An error ocurred" });
            }

            // Get UserId
            string userId = User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

            var publicDeclaration = new PublicDeclaration();

            publicDeclaration.Title = model.Title;
            publicDeclaration.Description = model.Description;
            publicDeclaration.UserId = new Guid(userId);
            publicDeclaration.SubmittedDate = DateTime.Now;
            publicDeclaration.DeclarationDate = model.DeclarationDate;
            publicDeclaration.State = model.State;

            // Signatures
            var signatureList = new List<PublicDeclarationSignature>();

            // Add
            foreach (var signature in model.SignatureList)
            {
                signatureList.Add(new PublicDeclarationSignature
                {
                    PersonName = signature.PersonName
                });
            }

            publicDeclaration.PublicDeclarationSignatureList = signatureList;

            await _db.PublicDeclaration.AddAsync(publicDeclaration);
            var result = await _db.SaveChangesAsync();

            //todo what do the result numbers mean?

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(PublicDeclarationModel model)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "An error ocurred" });
            }

            if (model.Id == null || model.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Public Declaration not specified" });
            }

            // Get UserId
            string userId = User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

            PublicDeclaration publicDeclaration = await _db.PublicDeclaration.Include(m => m.PublicDeclarationSignatureList).FirstOrDefaultAsync(u => u.Id == model.Id.Value);

            if (publicDeclaration == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Public Declaration does not exist" });
            }

            publicDeclaration.Title = model.Title;
            publicDeclaration.Description = model.Description;
            publicDeclaration.UserId = new Guid(userId);
            publicDeclaration.SubmittedDate = DateTime.Now;
            publicDeclaration.DeclarationDate = model.DeclarationDate;
            publicDeclaration.State = model.State;

            // Signatures
            var signatureList = publicDeclaration.PublicDeclarationSignatureList;

            // Remove
            var count = signatureList.Count;
            for (int i = 0; i < count; i++)
            {
                var signature = signatureList[i];

                if (!model.SignatureList.Exists(x => x.Id == signature.Id))
                {
                    _db.PublicDeclarationSignature.Remove(signature);
                }
            }

            // Add or Update
            foreach (var signature in model.SignatureList)
            {
                var existingSignature = signatureList.Where(x => x.Id == signature.Id).FirstOrDefault();

                if (existingSignature != null)
                {
                    existingSignature.PersonName = signature.PersonName;
                }
                else
                {
                    signatureList.Add(new PublicDeclarationSignature
                    {
                        PersonName = signature.PersonName
                    });
                }
            }

            var result = await _db.SaveChangesAsync();

            //todo what do the result numbers mean?

            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var publicDeclaration = await _db.PublicDeclaration.FirstOrDefaultAsync(u => u.Id == id);

            if (publicDeclaration == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Public Declaration does not exist" });
            }

            // Check if user allowed to delete
            string userId = User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;

            if(publicDeclaration.UserId != new Guid(userId))
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User not authorized to delete" });
            }

            // Delete
            _db.PublicDeclaration.Remove(publicDeclaration);

            var result = await _db.SaveChangesAsync();
            return Ok(result);
        }

    }
}
