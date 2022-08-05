﻿using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Services;
using System;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    public class DoctorController : Controller {

        private readonly IDoctorServices _doctorService;

        public DoctorController(IDoctorServices doctorService) {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Index() {
            ViewData["Title"] = "Listagem de medicos ativos";
            var list = await _doctorService.FindAllActiveAsync();
            return View(list);
        }

        public async Task<IActionResult> All() {
            ViewData["Title"] = "Listagem de medicos desativados";
            var list = await _doctorService.FindAllDisableAsync();
            return View("Index", list);
        }

        public IActionResult New() {
            return View();
        }

        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Doctor doctor = await _doctorService.FindByIdAsync(id.Value);
            if (doctor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        public async Task<IActionResult> Disable(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Doctor doctor = await _doctorService.FindByIdAsync(id.Value);
            if (doctor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        public async Task<IActionResult> Details(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Doctor doctor = await _doctorService.FindByIdAsync(id.Value);
            if (doctor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Doctor doctor) {
            try {
                if (!ModelState.IsValid) {
                    return View(doctor);
                }
                doctor.Status = UserStatus.Ativo;
                await _doctorService.InsertAsync(doctor);
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable(int id) {

            try {
                Doctor doctor = await _doctorService.FindByIdAsync(id);
                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                doctor.Status = UserStatus.Inativado;
                await _doctorService.UpdateAsync(doctor);
                TempData["SuccessMessage"] = "Usuario desativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enable(int id) {

            try {
                Doctor doctor = await _doctorService.FindByIdAsync(id);
                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                doctor.Status = UserStatus.Ativo;
                await _doctorService.UpdateAsync(doctor);
                TempData["SuccessMessage"] = "Usuario ativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Doctor doctor) {
            try {
                if (!ModelState.IsValid) {
                    return View(doctor);
                }
                int id = (int)doctor.Id;
                Doctor dbDoctor = await _doctorService.FindByIdAsync(id);
                if (dbDoctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbDoctor.Phone = doctor.Phone;
                dbDoctor.Email = doctor.Email;
                dbDoctor.Street = doctor.Street;
                dbDoctor.District = doctor.District;
                dbDoctor.State = doctor.State;
                dbDoctor.City = doctor.City;
                dbDoctor.Complement = doctor.Complement;
                dbDoctor.Number = doctor.Number;
                dbDoctor.Crm = doctor.Crm;
                dbDoctor.Speciality = doctor.Speciality;
                dbDoctor.Name = doctor.Name;

                await _doctorService.UpdateAsync(dbDoctor);
                TempData["SuccessMessage"] = "Usuario desativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
