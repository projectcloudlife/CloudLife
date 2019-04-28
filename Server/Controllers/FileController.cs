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
            var files = await _fileService.GetFiles(this.UserId());
            return new JsonResult(files);
        }

        [HttpPost()]
        [RequestSizeLimit(100_000_000_000)]
        [Route("upload")]
        public async Task<ActionResult<FileCommon>> UploadFile([FromBody] FileCommon file)
        {
            file.UserId = this.UserId();
            var uploadedFile = await _fileService.UploadFile(file.ToDB());
            return new JsonResult(uploadedFile);
        }

        [HttpDelete()]
        public async Task<ActionResult<bool>> DeleteFile([FromQuery] int fileId)
        {
            var file = new FileCommon
            {
                Id = fileId,
                UserId = this.UserId(),
            };
            var success = await _fileService.DeleteFile(file);
            return new JsonResult(success);
        }

        [HttpPost]
        [Route("download")]
        public async Task<ActionResult<FileCommon>> Download([FromBody] FileCommon file)
        {
            file.UserId = this.UserId();
            var downloadFile = await _fileService.DownloadFile(file);
            return new JsonResult(downloadFile);
        }

        [HttpPut]
        public async Task<ActionResult<FileCommon>> UpadeFileMetadata([FromBody] FileCommon file)
        {
            file.UserId = this.UserId();
            var updatedFile = await _fileService.UpadateFileMetadata(file);
            return new JsonResult(updatedFile);
        }

    }
}