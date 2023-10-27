using Microsoft.AspNetCore.Mvc;
using MVCPracticaArqdSoft.Data;
using MVCPracticaArqdSoft.Models;

namespace MVCPracticaArqdSoft.Controllers {

    public class ContactController : Controller {
        ContactData contactData = new ContactData();

        public IActionResult ViewContacts () {
            var contacts = contactData.FindAll();
            return View(contacts);
        }

        [HttpGet]
        public IActionResult CreateNewContact () {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNewContact (ContactModel newContact) {
            if (!ModelState.IsValid) return View();

            bool result = contactData.SaveOne(newContact);
            if (result) return RedirectToAction("ViewContacts");

            return View();
        }

        [HttpGet]
        public IActionResult UpdateContact (int contactId) {
            ContactModel currentContact = contactData.FindOne(contactId);
            return View(currentContact);
        }

        [HttpPost]
        public IActionResult UpdateContact (ContactModel newContactData) {
            if (!ModelState.IsValid) return View(newContactData);

            bool result = contactData.UpdateOne(newContactData.Id, newContactData);
            if (result) return RedirectToAction("ViewContacts");

            return View(newContactData);
        }

        [HttpGet]
        public IActionResult DeleteContact(int contactId) {
            ContactModel contact = contactData.FindOne(contactId);
            return View(contact);
        }

        [HttpPost]
        public IActionResult DeleteContact(ContactModel contact) {
            bool result = contactData.DeleteOne(contact.Id);
            if (result) return RedirectToAction("ViewContacts");
            return View(contact);
        }
    }

}
