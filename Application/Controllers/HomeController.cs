using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly IToDoRepository _toDoRepository;

        public HomeController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public IActionResult Index()
        {
            return View(_toDoRepository.GetAll());
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Entities.ToDo obj)
        {
            if (ModelState.IsValid)
            {
                _toDoRepository.Add(obj);
                Notification.Set(TempData, new Notificacao() { Mensagem = "A tarefa foi cadastrada com sucesso!", Tipo = TipoNotificacao.success });
                return View("Index", _toDoRepository.GetAll());
            }
            else
            {
                Notification.Set(TempData, new Notificacao() { Mensagem = "Não foi possível cadastrar essa tarefa", Tipo =  TipoNotificacao.danger});
                return View();
            }
        }

        public IActionResult Editar(int id)
        {
            return View(_toDoRepository.Get(id));
        }
        [HttpPost]
        public IActionResult Editar(Entities.ToDo obj)
        {

            if (ModelState.IsValid)
            {
                Notification.Set(TempData, new Notificacao() { Mensagem = "A tarefa foi alterada com sucesso!", Tipo = TipoNotificacao.success });
                _toDoRepository.Update(obj);
                return View("Index", _toDoRepository.GetAll());
            }
            else
            {
                Notification.Set(TempData, new Notificacao() { Mensagem = "Não foi possível editar essa tarefa", Tipo = TipoNotificacao.danger });
                return View();
            }
        }

        public IActionResult Remover(int id)
        {
            return View(_toDoRepository.Get(id));
        }

        public IActionResult ConfirmaRemover(int id)
        {
            _toDoRepository.Remove(_toDoRepository.Get(id));
            return View("Index", _toDoRepository.GetAll());
        }
    }
}
