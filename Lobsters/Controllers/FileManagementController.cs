using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.Repository;

namespace Lobsters.Controllers
{
    [RoutePrefix("file-management")]
    public class FileManagementController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        #region Campaigns

        public PartialViewResult AddEditCampaignPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? campaignId)
        {
            var model = unitOfWork.CampaignsRepo.Find(m => m.Id == campaignId);
            return PartialView(model);
        }
        public PartialViewResult TokenboxQuestionsPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? campaignId)
        {
            var questionsInCampaign = unitOfWork.QuestionsRepo.Get(m => m.CampaignId == campaignId);
            var questions = unitOfWork.QuestionsRepo.Get(m => m.CampaignId == null);
            ViewBag.QuestionsInCampaign = questionsInCampaign;
            return PartialView(questions);
        }

        [Route("campaigns")]
        public ActionResult Campaigns()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult CampaignsGridViewPartial()
        {
            var model = unitOfWork.CampaignsRepo.Get();
            return PartialView("_CampaignsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CampaignsGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Campaigns item, [ModelBinder(typeof(DevExpressEditorsBinder))]List<string> questionsList)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var i in questionsList)
                    {
                        var id = Convert.ToInt32(i);
                        item.Questions.Add(unitOfWork.QuestionsRepo.Find(m => m.Id == id));
                    }
                    unitOfWork.CampaignsRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.CampaignsRepo.Get();
            return PartialView("_CampaignsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CampaignsGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Campaigns item, [ModelBinder(typeof(DevExpressEditorsBinder))]List<string> questionsList)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var campaign = unitOfWork.CampaignsRepo.Find(m => m.Id == item.Id, includeProperties: "Questions");
                    campaign.Campaign = item.Campaign;

                    foreach (var i in questionsList)
                    {
                        var id = Convert.ToInt32(i);
                        campaign.Questions.Add(unitOfWork.QuestionsRepo.Find(m => m.Id == id));
                    }
                    // Insert here a code to update the item in your model
                    //unitOfWork.CampaignsRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.CampaignsRepo.Get();
            return PartialView("_CampaignsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CampaignsGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.CampaignsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.CampaignsRepo.Get();
            return PartialView("_CampaignsGridViewPartial", model);
        }
        #endregion

        #region Questions

        public PartialViewResult TokenboxChoicesPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? questionId)
        {
            var choicesList = unitOfWork.ChoicesRepo.Get(m => m.QuestionId == null);
            var choices = unitOfWork.ChoicesRepo.Get(m=>m.QuestionId==questionId);
            ViewBag.ChoiceList = choicesList;
            return PartialView(choices);
        }
        public PartialViewResult AddEditQuestionPartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? questionId)
        {
            var model = unitOfWork.QuestionsRepo.Find(m => m.Id == questionId);
            return PartialView(model);
        }
        public ActionResult Questions()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult QuestionGridViewPartial()
        {
            var model = unitOfWork.QuestionsRepo.Get(includeProperties: "Campaigns");
            return PartialView("_QuestionGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Questions item, [ModelBinder(typeof(DevExpressEditorsBinder))]List<string> choicesList)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var i in choicesList)
                    {
                        var id = Convert.ToInt32(i);
                        item.Choices.Add(unitOfWork.ChoicesRepo.Find(m => m.Id == id));
                    }
                    unitOfWork.QuestionsRepo.Insert(item);
                    unitOfWork.Save();
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.QuestionsRepo.Get(includeProperties: "Campaigns");
            return PartialView("_QuestionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Questions item, [ModelBinder(typeof(DevExpressEditorsBinder))]List<string> choicesList)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var question = unitOfWork.QuestionsRepo.Find(m => m.Id == item.Id);
                    question.Question = item.Question;
                    foreach (var i in choicesList)
                    {
                        var id = Convert.ToInt32(i);
                        question.Choices.Add(unitOfWork.ChoicesRepo.Find(m => m.Id == id));
                    }
                    unitOfWork.Save();
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.QuestionsRepo.Get(includeProperties: "Campaigns");
            return PartialView("_QuestionGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult QuestionGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))]int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    unitOfWork.QuestionsRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.QuestionsRepo.Get(includeProperties: "Campaigns");
            return PartialView("_QuestionGridViewPartial", model);
        }
        #endregion

        #region Choices
        public ActionResult Choices()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult ChoicesGridViewPartial()
        {
            var model = unitOfWork.ChoicesRepo.Get(includeProperties: "Questions,Questions.Campaigns");
            return PartialView("_ChoicesGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult ChoicesGridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Choices item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    unitOfWork.ChoicesRepo.Insert(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ChoicesRepo.Get(includeProperties: "Questions,Questions.Campaigns");
            return PartialView("_ChoicesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ChoicesGridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] Models.Choices item)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    unitOfWork.ChoicesRepo.Update(item);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.ChoicesRepo.Get(includeProperties: "Questions,Questions.Campaigns");
            return PartialView("_ChoicesGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult ChoicesGridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] int? Id)
        {

            if (Id >= 0)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    unitOfWork.ChoicesRepo.Delete(m => m.Id == Id);
                    unitOfWork.Save();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = unitOfWork.ChoicesRepo.Get(includeProperties: "Questions,Questions.Campaigns");
            return PartialView("_ChoicesGridViewPartial", model);
        }

        public PartialViewResult AddEditChoicePartial([ModelBinder(typeof(DevExpressEditorsBinder))]int? choiceId)
        {
            var model = unitOfWork.ChoicesRepo.Find(m => m.QuestionId == null);
         
            return PartialView(model);
        }
        #endregion


    }
}