using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MpdaTest.BdModels;
using MpdaTest.Models;
using MpdaTest.Models.PassingModels;
using MpdaTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MpdaTest.Controllers
{


    public class HomeController : Controller
    {
        string loginMain = null;

        Model1 BD = new Model1();
        CookieOptions cookieOptions = new CookieOptions();
        public HomeController()
        {

        }
        #region Методы администратора

        //Отображение панели администратора ✔
        public IActionResult AdminPanel()
        {
            BD = new Model1();
            AdminViewModel model = new AdminViewModel();
            model.listTests = new List<ListTestAdmin>();

            foreach (var item in BD.TestSistem.ToList())
            {
                ListTestAdmin testAdmin = new ListTestAdmin() { ID = item.ID, Name = item.Name };
                testAdmin.AllCount = BD.UserSelectTest.Where(x => x.TestID == item.ID).Count();
                testAdmin.Count = BD.UserSelectTest.Where(x => x.TestID == item.ID && x.BoolPassed == true).Count();
                testAdmin.date = "Дата(" + item.DateOpen + "/" + item.DateClose;
                model.listTests.Add(testAdmin);
            }





            return View(model);
        }

        //Добавление нового теста ✔
        public IActionResult AddNewTest(AdminViewModel model)
        {
            TestSistem testSistem = new TestSistem() { Name = model.NewTestModel.Name, DateOpen = model.NewTestModel.DateOpen.ToShortDateString(), DateClose = model.NewTestModel.DateClose.ToShortDateString() };
            BD.TestSistem.Add(testSistem);
            BD.SaveChanges();

            return RedirectToAction("AdminPanel", "Home");
        }

        //Копирование теста ✔
        public async Task<IActionResult> CopyTest(AdminViewModel model)
        {


            //Копирование теста
            var Test =  BD.TestSistem.Where(x => x.ID == model.CopyingTestModel.IdTest).FirstOrDefault();


            if (Test != null)
            {
                var testSistem = new TestSistem
                {
                    DateClose = model.CopyingTestModel.DateClose.ToShortDateString(),
                    DateOpen = model.CopyingTestModel.DateOpen.ToShortDateString(),
                    Name = model.CopyingTestModel.Name
                };

                BD.TestSistem.Add(testSistem);
                BD.SaveChanges();

                //Копирование описания
                var opis = await BD.OpisTheme.Where(x => x.IdTheme == Test.ID).FirstOrDefaultAsync();
                if (opis != null)
                {
                    var opisTest = new OpisTheme
                    {
                        IdTheme = testSistem.ID,
                        ImageBit = opis.ImageBit,
                        link = opis.link,
                        Opis = opis.Opis
                    };
                    BD.OpisTheme.Add(opisTest);
                }

                //Копирование темы
                foreach (var item in await BD.ThemeTest.Where(x => x.IDTestSistem == Test.ID).ToListAsync())
                {
                    ThemeTest themeTest = new ThemeTest() { IDTestSistem = testSistem.ID, Name = item.Name };
                    BD.ThemeTest.Add(themeTest);
                    BD.SaveChanges();

                    var sort = await BD.TestSort.Where(x => x.IDtheme == item.ID).ToListAsync();
                    foreach (var itemSort in sort)
                    {
                        switch (itemSort.Type)
                        {
                            case "CloseTest":

                                var close = await BD.TestAnswer.Where(x => x.ID == itemSort.IDques).FirstOrDefaultAsync();

                                if (close != null)
                                {
                                    TestAnswer testAnswer = new TestAnswer() { IDTheme = themeTest.ID, necessarily = close.necessarily, Question = close.Question };
                                    BD.TestAnswer.Add(testAnswer);
                                   await BD.SaveChangesAsync();

                                    TestSort testSort = new TestSort() { IDques = testAnswer.ID, IDtheme = themeTest.ID, Number = itemSort.Number, Type = itemSort.Type };
                                    BD.TestSort.Add(testSort);

                                    foreach (var itemAnswer in BD.AnswerT.Where(x => x.IDTestAnswer == close.ID).ToList())
                                    {
                                        AnswerT answerT = new AnswerT() { Correct = itemAnswer.Correct, NumberOfSelected = 0, Text = itemAnswer.Text, TextUser = itemAnswer.TextUser, IDTestAnswer = testAnswer.ID };
                                        BD.AnswerT.Add(answerT);

                                    }
                                   await BD.SaveChangesAsync();
                                }

                                break;
                            case "OpenTest":

                                var Open = await BD.TestOpen.Where(x => x.ID == itemSort.IDques).FirstOrDefaultAsync();

                                if (Open != null)
                                {
                                    TestOpen testOpen = new TestOpen() { IDTheme = themeTest.ID, necessarily = Open.necessarily, Question = Open.Question };
                                    BD.TestOpen.Add(testOpen);
                                    await BD.SaveChangesAsync();


                                    TestSort testSort1 = new TestSort() { IDques = testOpen.ID, IDtheme = themeTest.ID, Number = itemSort.Number, Type = itemSort.Type };
                                    BD.TestSort.Add(testSort1);
                                    await BD.SaveChangesAsync();
                                }

                                break;

                            case "TableTest":
                                var Table = await BD.TableTest.Where(x => x.ID == itemSort.IDques).FirstOrDefaultAsync();

                                if (Table != null)
                                {
                                    TableTest tableTest = new TableTest() { Desp = Table.Desp, Name = Table.Name, necessarily = Table.necessarily, IDTheme = themeTest.ID };
                                    BD.TableTest.Add(tableTest);
                                    await BD.SaveChangesAsync();

                                    TestSort testSort2 = new TestSort() { IDques = tableTest.ID, IDtheme = themeTest.ID, Number = itemSort.Number, Type = itemSort.Type };
                                    BD.TestSort.Add(testSort2);

                                    foreach (var itemTheme in await BD.Theme.Where(x => x.IDTable == Table.ID).ToListAsync())
                                    {
                                        Theme theme = new Theme() { IDTable = tableTest.ID, Text = itemTheme.Text };
                                        BD.Theme.Add(theme);
                                    }
                                    foreach (var itemQuestion in await BD.question.Where(x => x.IDTable == Table.ID).ToListAsync())
                                    {
                                        question Question = new question() { IDTable = tableTest.ID, Text = itemQuestion.Text };
                                        BD.question.Add(Question);
                                    }
                                    await BD.SaveChangesAsync();
                                }



                                break;

                        }
                    }





                }
            }


            return RedirectToAction("AdminPanel", "Home");
        }

        public IActionResult CreateUser()
        {
            return RedirectToAction("AdminPanel", "Home");
        }


        #endregion
        //Проверка логина в Coockie ✔
        public string CoockiesChek()
        {
            if (Request.Cookies.ContainsKey("Login"))
            {
                var login = Request.Cookies["Login"];

                if (BD.UserSelectTest.Where(x => x.Login.ToLower() == login.ToLower()).Any())
                {
                    return login;
                }
                else
                {
                    Response.Cookies.Delete("Login");
                    return null;
                }
            }
            return null;

        }

        //Отправка ответов на тест ✔
        [HttpPost]
        public IActionResult PassingSet(PassingTestModel model)
        {


            foreach (var item in model.passingThemes)
            {
                if (item.openPassingsList != null)
                {
                    foreach (var item2 in item.openPassingsList)
                    {
                        if (item2.Text != null)
                        {
                            if (item2.Text.Replace(" ", "") != "")
                            {
                                AnswerOpenTest answerOpenTest = new AnswerOpenTest() { Answer = item2.Text, IDTestOpen = item2.ID };
                                BD.AnswerOpenTest.Add(answerOpenTest);
                            }
                        }



                    }
                }

                if (item.ClosePassingsList != null)
                {
                    foreach (var item2 in item.ClosePassingsList)
                    {
                        var a = BD.AnswerT.Where(x => x.ID == item2.AnswerTSelect).FirstOrDefault();
                        a.NumberOfSelected++;

                    }
                }
                if (item.TablePassings != null)
                {
                    foreach (var item2 in item.TablePassings)
                    {
                        foreach (var item3 in item2.TableThemes)
                        {

                            foreach (var item4 in item3.TableQues)
                            {
                                if (item4.Count != 0)
                                {
                                    AnswerTheme answer = new AnswerTheme() { AnswerText = item4.Count.ToString(), IDQuestion = item4.ID, IDTheme = item3.ID };
                                    BD.AnswerTheme.Add(answer);
                                }


                            }


                        }


                    }
                }




            }
            // BD.UserSelectTest.Where(x => x.Login.ToLower() == loginMain.ToLower()).FirstOrDefault().BoolPassed = true;
            BD.SaveChanges();


            return RedirectToAction("PassingTheTest", "Home");
        }

        //Отображение прохождения тестов ✔
        public async Task<IActionResult> PassingTheTest()
        {


            loginMain = CoockiesChek();
            if (loginMain != null)
            {
                var User = BD.UserSelectTest.Where(x => x.Login.ToLower() == loginMain.ToLower()).FirstOrDefault();

                var TestOpis = BD.OpisTheme.Where(x => x.IdTheme == User.TestID).FirstOrDefault();
                var Test = BD.TestSistem.Where(x => x.ID == User.TestID).FirstOrDefault();
                PassingTestModel passingTest = new PassingTestModel();
                passingTest.Opisanie = TestOpis.Opis;
                passingTest.passingThemes = new List<PassingThemeModel>();
                passingTest.Name = Test.Name;
                passingTest.ID = Test.ID;
                passingTest.bytes = TestOpis.ImageBit;
                passingTest.url = TestOpis.link;







                List<PassingThemeModel> passingTheme = new List<PassingThemeModel>();


                foreach (var itemTheme in BD.ThemeTest.Where(x => x.IDTestSistem == User.TestID))
                {
                    var Sort = from p in BD.TestSort.Where(x => x.IDtheme == itemTheme.ID).ToList()
                               orderby p.Number ascending
                               select p;

                    PassingThemeModel passingThemeModel = new PassingThemeModel();
                    passingThemeModel.Name = itemTheme.Name;
                    passingThemeModel.ID = itemTheme.ID;
                    passingThemeModel.testSortsList = Sort.ToList();
                    passingThemeModel.openPassingsList = new List<OpenPassing>();
                    passingThemeModel.ClosePassingsList = new List<ClosePassing>();
                    passingThemeModel.TablePassings = new List<TablePassing>();



                    foreach (var itemSort in Sort)
                    {
                        switch (itemSort.Type)
                        {
                            case "CloseTest":

                                var close = BD.TestAnswer.Where(x => x.ID == itemSort.IDques).FirstOrDefault();

                                if (close != null)
                                {
                                    ClosePassing closePassing = new ClosePassing();
                                    closePassing.Id = close.ID;
                                    closePassing.Name = close.Question;
                                    closePassing.answerTs = BD.AnswerT.Where(x => x.IDTestAnswer == close.ID).ToList();


                                    passingThemeModel.ClosePassingsList.Add(closePassing);
                                }
                                else
                                {
                                    BD.TestSort.Remove(itemSort);

                                }
                                break;

                            case "OpenTest":

                                var Open = BD.TestOpen.Where(x => x.ID == itemSort.IDques).FirstOrDefault();
                                if (Open != null)
                                {
                                    OpenPassing openPassing = new OpenPassing();
                                    openPassing.Name = Open.Question;
                                    openPassing.Text = string.Empty;
                                    openPassing.ID = Open.ID;

                                    passingThemeModel.openPassingsList.Add(openPassing);
                                }
                                else
                                {
                                    BD.TestSort.Remove(itemSort);





                                }

                                break;

                            case "TableTest":
                                var Table = BD.TableTest.Where(x => x.ID == itemSort.IDques).FirstOrDefault();

                                if (Table != null)
                                {
                                    TablePassing tablePassing = new TablePassing();
                                    tablePassing.Id = Table.ID;
                                    tablePassing.Opisanie = Table.Desp;
                                    tablePassing.Name = Table.Name;
                                    tablePassing.IsRec = Table.necessarily;
                                    tablePassing.TableThemes = new List<TableThemePassing>();

                                    foreach (var itemThemeTable in BD.Theme.Where(x => x.IDTable == Table.ID).ToList())
                                    {
                                        TableThemePassing themePassing = new TableThemePassing();
                                        themePassing.ID = itemThemeTable.ID;
                                        themePassing.Name = itemThemeTable.Text;
                                        themePassing.TableQues = new List<TableQuesPassing>();
                                        foreach (var itemQuesTable in BD.question.Where(x => x.IDTable == Table.ID).ToList())
                                        {
                                            TableQuesPassing quesPassing = new TableQuesPassing();
                                            quesPassing.ID = itemQuesTable.ID;
                                            quesPassing.Name = itemQuesTable.Text;
                                            themePassing.TableQues.Add(quesPassing);
                                        }
                                        tablePassing.TableThemes.Add(themePassing);

                                    }

                                    passingThemeModel.TablePassings.Add(tablePassing);
                                }
                                else
                                {
                                    BD.TestSort.Remove(itemSort);
                                }
                                break;

                        }
                    }
                    BD.SaveChangesAsync();
                    passingTest.passingThemes.Add(passingThemeModel);



                }

                return View(passingTest);
            }
            return RedirectToAction("vhod", "Home");

        }

        //Вход в аккаунт ✔
        public IActionResult vhod(LoginViewModel model)
        {
            if (CoockiesChek() == null)
            {
                return View(model);
            }
            else
            {
                loginMain = CoockiesChek();
                if (loginMain != "Admin")
                {
                    return RedirectToAction("PassingTheTest", "Home");//Переход на нужную страницу
                }
                else
                {
                    return RedirectToAction("AdminPanel", "Home");//Переход на нужную страницу
                }
            }

        }

        //Отправка данных для входа в аккаунт ✔
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {

                if (BD.UserSelectTest.Where(x => x.Login.ToLower() == model.Login.ToLower()).Any())
                {
                    var login = BD.UserSelectTest.Where(x => x.Login.ToLower() == model.Login.ToLower()).FirstOrDefault().Login;

                    var result = BD.UserSelectTest.Where(x => x.Login.ToLower() == login && x.Password == model.Password).Any();
                    if (result)
                    {
                        cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(7));
                        Response.Cookies.Append("Login", login, cookieOptions);
                        loginMain = login;

                        if (login != "Admin")
                        {
                            return RedirectToAction("PassingTheTest", "Home");//Переход на нужную страницу
                        }
                        else
                        {
                            return RedirectToAction("AdminPanel", "Home");//Переход на нужную страницу

                        }

                    }
                    else
                    {
                        model.Mess = "Неправильный логин и (или) пароль";
                    }
                }
                else
                {
                    model.Mess = "Неправильный логин и (или) пароль";
                }


            }
            return RedirectToAction("vhod", "Home", model);//Переход на нужную страницу

        }

        //ВЫход из аккаунта ✔
        public IActionResult Exit()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Response.Cookies.Delete("Login");
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
