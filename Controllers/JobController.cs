using FoodHub.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tashtibaat.Data;
using Tashtibaat.Helpers;
using Tashtibaat.Models;

namespace Tashtibaat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly ISendMail _sendMail;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public JobController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment , UserManager<Users> userManager, ISendMail sendMail)
        {
            _context = context;
            _userManager = userManager;
            _sendMail = sendMail;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<ActionResult> PostJob(JobDto jobDto)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByNameAsync(userName);
            var job = new Job 
            {
                Address = jobDto.Address,
                Age = jobDto.Age,
                City = jobDto.City,
                JobType = jobDto.JobType,
                Name = jobDto.Name,
                PhoneNum = jobDto.PhoneNum,
                CriminalRecordPic = FileHelper.UploadedFile(jobDto.CriminalRecordPic, _webHostEnvironment.WebRootPath, "TODO: fix ngrok to get a fixed domain", "CriminalRecord"),
                PersonalPic = FileHelper.UploadedFile(jobDto.PersonalPic, _webHostEnvironment.WebRootPath, "TODO: fix ngrok to get a fixed domain", "Personal"),
                IDLowerPic = FileHelper.UploadedFile(jobDto.IDLowerPic, _webHostEnvironment.WebRootPath, "TODO: fix ngrok to get a fixed domain", "IDLower"),
                IDUppperPic = FileHelper.UploadedFile(jobDto.IDUppperPic, _webHostEnvironment.WebRootPath, "TODO: fix ngrok to get a fixed domain", "IDUppper"),
                user = user
            };

            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();

            var status = _sendMail.Send(user, "Job", "Someone ask for a job");

            if (status)
            {
                return Ok(new
                {
                    Data = new { },
                    Status = true,
                    Message = "Success"
                });
            }
            else
            {
                return BadRequest(new
                {
                    Data = new { },
                    Status = false,
                    Message = "please try again later"
                });
            }
        }

    }
}
