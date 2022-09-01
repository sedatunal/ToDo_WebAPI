using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo_WebAPI.Context;
using ToDo_WebAPI.Entities;
using ToDo_WebAPI.Models;

namespace ToDo_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDosController : ControllerBase
    {
        //CRUD İşlemleri Yapılacak

        //Get -> kayıt veya kayıtları çekmek.
        //POST -> yeni bit kayıt.
        //PUT -> veri güncelleme.
        //PATCH -> bir kaydın bazı propertyleri değişiyorsa.
        //DELETE -> bir kaydı silmek.

        // 200 ok
        // 201 Created

        // 400 Hatalı İstek (Bad Request)
        // 401 Giriş Yapılmamış, login olunmamış.
        // 403 Giriş yapılmış ama kullanıcı yetkisi yok
        // 404 endpoint bulunamadı.

        // 500 sunucu hatası

        private readonly ToDoContext _toDoContext;
        public ToDosController(ToDoContext toDoContext)
        {
            _toDoContext = toDoContext;
        }

        [HttpGet]
        public IActionResult GetAllToDos()
        {
            var entity = _toDoContext.ToDos.ToList();
            return Ok(entity); // 200 başarılı kodu ve Json halinde verileri döndüm.
        }

        [HttpGet("{id}")]
        public IActionResult GetToDo(int id)
        {
            var entity = _toDoContext.ToDos.Find(id);

            if(entity == null)
            {
                return NotFound(); // return StatusCode 404
            }
            return Ok(entity);
        }

        [HttpPost]
        public IActionResult AddToDo(AddToDoRequest request)
        {
            var entity = new ToDoEntity
            {
                Title = request.Title,
                Content = request.Content,
                IsDone = false
            };
            _toDoContext.ToDos.Add(entity);
            try
            {
                _toDoContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError); // return StatusCode(500) olarak da yazılabilir.
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDo(int id, UpdateToDoRequest request)
        {
            var entity = _toDoContext.ToDos.Find(id);

            if (entity == null)
            {
                return NotFound();
            }

            entity.Title=request.Title;
            entity.Content=request.Content;
            entity.IsDone = request.IsDone;

            _toDoContext.ToDos.Update(entity);

            try
            {
                _toDoContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult CheckToDo(int id , CheckToDoRequest request)
        {
            var entity = _toDoContext.ToDos.Find(id);
            if (entity==null)
            {
                return NotFound();
            }

            entity.IsDone = request.IsDone;
            _toDoContext.ToDos.Update(entity);
            try
            {
                _toDoContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteToDo(int id)
        {
            var entity = _toDoContext.ToDos.Find(id);

            if (entity==null)
            {
                return BadRequest();
            }
            _toDoContext.ToDos.Remove(entity);
            try
            {
                _toDoContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }


    }

}
