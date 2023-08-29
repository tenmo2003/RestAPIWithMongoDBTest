using System;
using Microsoft.AspNetCore.Mvc;
using MongoAPI.Services;
using MongoAPI.Models;

namespace MongoAPI.Controllers;

[Controller]
[Route("api/[controller]")]
public class TestController : Controller {
    
    private readonly MongoDBService _mongoDBService;

    public TestController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Test>> Get() {
        return await _mongoDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Test test) {
        await _mongoDBService.CreateAsync(test);
        return CreatedAtAction(nameof(Get), new { id = test.Id }, test);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Test test) {
        await _mongoDBService.UpdateAsync(id, test);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);

        return NoContent();
    }

}