using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.DAL.Interfaces;
using Server.DAL.Repositories;
using Server.Extantions;
using Server.Interfaces;

namespace Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        IFileService _fileService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileCommon>>> GetFile()
        {
            var files = await _fileService.GetFiles( this.UserId(),false);
            return new JsonResult(files);
        }

        [HttpPost]
        public async Task<ActionResult<int>> UploadFile([FromBody] FileCommon file)
        {
            file.UserId = this.UserId();
            var fileId = await _fileService.UploadFile(file.ToDB());
            return new JsonResult(fileId);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> DeleteFile([FromBody] FileCommon file)
        {


            file.UserId = this.UserId();
            var success = await _fileService.DeleteFile(file);
            return new JsonResult(success);
        } 

        [HttpPost]
        public async Task<ActionResult<FileCommon>> Download([FromBody] FileCommon file)
        {
            file.UserId = this.UserId();
            var downloadFile = await _fileService.DownloadFile(file);
            return new JsonResult(downloadFile);
        }

    }
}