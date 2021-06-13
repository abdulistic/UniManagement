﻿using Microsoft.AspNetCore.Mvc;
using Restaurant.ClassLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniManagementApi.Services;
using static UniManagementApi.SupportClasses.Utility;

namespace UniManagementApi.Controllers
{
    [Produces("application/json")]
    public class TeacherController : Controller
    {
        private readonly ITeacherService service;

        public TeacherController(ITeacherService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<List<SubjectVM>> GetSubjectList(int id)
        {
            return await service.GetSubjectList(id);
        }

        [HttpGet]
        public async Task<List<TestVM>> GetTestList(int id)
        {
            return await service.GetTestList(id);
        }

        [HttpGet]
        public async Task<Response> DeleteTestById(int id)
        {
            return await service.DeleteTestById(id);
        }

        [HttpPost]
        public async Task<Response> AddTest([FromBody]TestVM model)
        {
            return await service.AddTest(model);
        }

        [HttpPost]
        public async Task<Response> AddTestResult([FromBody] TestVM model)
        {
            return await service.AddTestResult(model);
        }


        [HttpGet]
        public async Task<TestVM> GetTestById(long id)
        {
            return await service.GetTestById(id);
        }

        [HttpGet]
        public async Task<TestMgtVM> GetTestResults(int id)
        {
            return await service.GetTestResults(id);
        }


    }
}
