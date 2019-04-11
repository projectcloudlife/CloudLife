using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.DAL.Repositories;
using Server.Extantions;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {

        public FileController(FileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        FileRepository _fileRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileCommon>>> GetFile()
        {
            var userId = this.UserId();
            var files = await _fileRepository.GetWhere(file => file.UserId == userId);
            return new JsonResult(files.Select(file => file.ToCommon()));
        }

        [HttpPost]
        public async Task<ActionResult<int>> UploadFile([FromBody] FileCommon file)
        {
            var fileId = await _fileRepository.UploadFile(file.ToDB());
            return new JsonResult(fileId);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> DeleteFile([FromBody] int fileId)
        {
            var success = await _fileRepository.DeleteFile(fileId);
            return new JsonResult(success);
        } 

        [HttpPost]
        public async Task<ActionResult<FileCommon>> Download([FromBody] int fileId)
        {
            var file = await _fileRepository.DownloadFile(fileId);
            return new JsonResult(file);
        }

    }
}