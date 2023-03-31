﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MpdaTest.BdModels;
using MpdaTest.Models;
using MpdaTest.Models.EditingModels;
using MpdaTest.Models.PassingModels;
using MpdaTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
        #region Методы администратора ✔

        //Отображение панели администратора ✔
        //Добавить проверку на админа
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
        //Добавить проверку на админа
        public IActionResult AddNewTest(AdminViewModel model)
        {
            TestSistem testSistem = new TestSistem() { Name = model.NewTestModel.Name, DateOpen = model.NewTestModel.DateOpen.ToShortDateString(), DateClose = model.NewTestModel.DateClose.ToShortDateString() };
            BD.TestSistem.Add(testSistem);
            BD.SaveChanges();

            return RedirectToAction("AdminPanel", "Home");
        }

        //Копирование теста ✔
        //Добавить проверку на админа
        public async Task<IActionResult> CopyTest(AdminViewModel model)
        {


            //Копирование теста
            var Test = BD.TestSistem.Where(x => x.ID == model.CopyingTestModel.IdTest).FirstOrDefault();


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

        //Удаление теста ✔
        //Добавить проверку на админа
        public async Task<IActionResult> DeleteTest(int IDTestSistem)
        {

            foreach (var itemUsers in await BD.UserSelectTest.Where(x => x.TestID == IDTestSistem).ToListAsync())
            {
                BD.UserSelectTest.Remove(itemUsers);
            }

            var Opis = await BD.OpisTheme.Where(x => x.IdTheme == IDTestSistem).FirstOrDefaultAsync();
            if (Opis != null)
            {
                BD.OpisTheme.Remove(Opis);
            }


            foreach (var item in await BD.ThemeTest.Where(x => x.IDTestSistem == IDTestSistem).ToListAsync())
            {

                //Удаление открытого вопроса
                foreach (var itemOpen in await BD.TestOpen.Where(x => x.IDTheme == item.ID).ToListAsync())
                {
                    foreach (var itemOpenAnswer in await BD.AnswerOpenTest.Where(x => x.IDTestOpen == itemOpen.ID).ToListAsync())
                    {
                        BD.AnswerOpenTest.Remove(itemOpenAnswer);
                    }
                    BD.TestOpen.Remove(itemOpen);
                }

                //Удаление вопроса с выбором ответа
                foreach (var itemAnswer in await BD.TestAnswer.Where(x => x.IDTheme == item.ID).ToListAsync())
                {
                    foreach (var itemAnswerT in await BD.AnswerT.Where(x => x.IDTestAnswer == itemAnswer.ID).ToListAsync())
                    {
                        BD.AnswerT.Remove(itemAnswerT);
                    }
                    BD.TestAnswer.Remove(itemAnswer);
                }

                //удаление табличного вопроса
                foreach (var itemTable in await BD.TableTest.Where(x => x.IDTheme == item.ID).ToListAsync())
                {
                    foreach (var itemTheme in await BD.Theme.Where(x => x.IDTable == itemTable.ID).ToListAsync())
                    {
                        foreach (var itemAnswerTheme in await BD.AnswerTheme.Where(x => x.IDTheme == itemTheme.ID).ToListAsync())
                        {
                            BD.AnswerTheme.Remove(itemAnswerTheme);
                        }
                        BD.Theme.Remove(itemTheme);
                    }
                    foreach (var itemQuestion in await BD.question.Where(x => x.IDTable == itemTable.ID).ToListAsync())
                    {
                        BD.question.Remove(itemQuestion);
                    }
                    BD.TableTest.Remove(itemTable);
                }

                //Удаление Сортировочного листа
                foreach (var itemSort in await BD.TestSort.Where(x => x.IDtheme == item.ID).ToListAsync())
                {
                    BD.TestSort.Remove(itemSort);
                }

                BD.ThemeTest.Remove(item);
            }

            BD.TestSistem.Remove(await BD.TestSistem.Where(x => x.ID == IDTestSistem).FirstOrDefaultAsync());
            await BD.SaveChangesAsync();
            return RedirectToAction("AdminPanel", "Home");
        }

        //Добавление пользователей ✔
        //Добавить проверку на админа
        public IActionResult CreateUser(AdminViewModel model)
        {
            int num = this.BD.UserSelectTest.Count();
            string s = string.Empty;
            for (int index = 0; index < model.CreateUserModel.Count; ++index)
            {
                ++num;
                UserSelectTest entity = new UserSelectTest();
                entity.TestID = model.CreateUserModel.IdTest;
                entity.Login = model.CreateUserModel.UserName + num.ToString();
                entity.Password = this.GetPass();
                entity.BoolPassed = false;
                this.BD.UserSelectTest.Add(entity);
                s = s + "\n\nЛогин: " + entity.Login + "\nПароль: " + entity.Password;
            }
            this.BD.SaveChanges();
            Microsoft.Net.Http.Headers.ContentDispositionHeaderValue dispositionHeaderValue = new Microsoft.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
            {
                FileNameStar = "Список пользователей.txt"
            };
            this.Response.Headers.Add(HeaderNames.ContentDisposition, dispositionHeaderValue.ToString());
            return (IActionResult)this.File(Encoding.UTF8.GetBytes(s), "application/vnd.openxmlformats-officedocument.wordprocessingml.document");




        }

        #endregion

        #region Создание и удаление элементов в редактировании теста ✔

        //Создание описания в редакторе тестов ✔
        //Добавить проверку на админа
        [HttpPost]
        public IActionResult OpisMember(PassingTestModel model)
        {
            OpisTheme opisTheme = this.BD.OpisTheme.Where((x => x.IdTheme == model.OpisCreate.IDTestSistem)).FirstOrDefault();
            if (opisTheme == null)
            {
                OpisTheme entity = new OpisTheme()
                {
                    IdTheme = model.OpisCreate.IDTestSistem,
                    link = model.OpisCreate.Link,
                    Opis = model.OpisCreate.Opis
                };
                if (model.OpisCreate.FileBinar != null)
                {
                    byte[] numArray = null;
                    using (BinaryReader binaryReader = new BinaryReader(model.OpisCreate.FileBinar.OpenReadStream()))
                        numArray = binaryReader.ReadBytes((int)model.OpisCreate.FileBinar.Length);
                    entity.ImageBit = numArray;
                }
                this.BD.OpisTheme.Add(entity);
            }
            else
            {
                opisTheme.Opis = model.OpisCreate.Opis;
                opisTheme.link = model.OpisCreate.Link;
                opisTheme.IdTheme = model.OpisCreate.IDTestSistem;
                if (model.OpisCreate.FileBinar != null)
                {
                    byte[] numArray = (byte[])null;
                    using (BinaryReader binaryReader = new BinaryReader(model.OpisCreate.FileBinar.OpenReadStream()))
                        numArray = binaryReader.ReadBytes((int)model.OpisCreate.FileBinar.Length);
                    opisTheme.ImageBit = numArray;
                }
                else
                    opisTheme.ImageBit = (byte[])null;
            }
            this.BD.SaveChanges();
            return RedirectToAction("ChangingTest", "Home", (object)new
            {
                IdTest = model.OpisCreate.IDTestSistem
            });
        }


        //Создание темы в редакторе тестов ✔
        //Добавить проверку на админа
        public async Task<IActionResult> CreateTheme(PassingTestModel model)
        {
            ThemeTest NewTheme = new ThemeTest()
            {
                IDTestSistem = model.createTheme.IdTest,
                Name = model.createTheme.Name
            };
            BD.ThemeTest.Add(NewTheme);
            await BD.SaveChangesAsync();
            return RedirectToAction("ChangingTest", "Home", new { IdTest = model.createTheme.IdTest });
        }


        //Создание таблиц в редакторе тестов ✔
        //Добавить проверку на админа
        public IActionResult CreateTable(PassingTestModel model)
        {
            int idthem = model.CreateTable.IdTheme;
            if (model.CreateTable.TableDes.Replace(" ", "") != "" && model.CreateTable.TableName.Replace(" ", "") != "" && model.CreateTable.Themes.Replace(" ", "") != "" && model.CreateTable.Q.Replace(" ", "") != "")
            {
                TableTest entity1 = new TableTest()
                {
                    IDTheme = model.CreateTable.IdTheme,
                    Name = model.CreateTable.TableName,
                    Desp = model.CreateTable.TableDes,
                    necessarily = model.CreateTable.IsRec
                };
                this.BD.TableTest.Add(entity1);
                this.BD.SaveChanges();


                if (model.CreateTable.Themes !=null)
                {
                    string[] strArray1 = model.CreateTable.Themes.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (string str in strArray1)
                    {
                        this.BD.Theme.Add(new Theme()
                        {
                            IDTable = entity1.ID,
                            Text = str
                        });
                    }
                }
                if (model.CreateTable.Q !=null)
                {
                    string[] strArray2 = model.CreateTable.Q.Split(new[] { Environment.NewLine }, StringSplitOptions.None);




                    foreach (string str in strArray2)
                    {
                        this.BD.question.Add(new question()
                        {
                            IDTable = entity1.ID,
                            Text = str
                        });
                    }
                }
                

                this.BD.SaveChanges();

                if (BD.TestSort.Where(x => x.IDtheme == model.CreateTable.IdTheme).Any())
                {
                    var CountSort = BD.TestSort.Where(x => x.IDtheme == model.CreateTable.IdTheme).Count();
                    TestSort NewSort = new TestSort()
                    {
                        IDtheme = model.CreateTable.IdTheme,
                        IDques = entity1.ID,
                        Number = CountSort + 1,
                        Type = "TableTest"

                    };
                    BD.TestSort.Add(NewSort);
                }
                else
                {
                    TestSort NewSort = new TestSort()
                    {
                        IDtheme = model.CreateTable.IdTheme,
                        IDques = entity1.ID,
                        Number = 1,
                        Type = "TableTest"

                    };
                    BD.TestSort.Add(NewSort);
                }
                this.BD.SaveChanges();

            }
            return RedirectToAction("ChangingTest", "Home", new { IdTest = model.CreateTable.IdTest });
        }


        //Отображение редактирования теста ✔
        //Добавить проверку на админа
        public IActionResult CreateOpen(PassingTestModel model)
        {
            if (model.createOpen.Name.Replace(" ", "") != "")
            {
                int idthem = model.createOpen.IdTheme;
                TestOpen entity1 = new TestOpen()
                {
                    IDTheme = model.createOpen.IdTheme,
                    Question = model.createOpen.Name,
                    necessarily = model.createOpen.IsRec
                };
                this.BD.TestOpen.Add(entity1);
                this.BD.SaveChanges();

                if (BD.TestSort.Where(x => x.IDtheme == model.createOpen.IdTheme).Any())
                {
                    var CountSort = BD.TestSort.Where(x => x.IDtheme == model.createOpen.IdTheme).Count();
                    TestSort NewSort = new TestSort()
                    {
                        IDtheme = model.createOpen.IdTheme,
                        IDques = entity1.ID,
                        Number = CountSort + 1,
                        Type = "OpenTest"

                    };
                    BD.TestSort.Add(NewSort);
                }
                else
                {
                    TestSort NewSort = new TestSort()
                    {
                        IDtheme = model.createOpen.IdTheme,
                        IDques = entity1.ID,
                        Number = 1,
                        Type = "OpenTest"

                    };
                    BD.TestSort.Add(NewSort);
                }

                this.BD.SaveChanges();
            }
            return RedirectToAction("ChangingTest", "Home", new { IdTest = model.createOpen.IdTest });
        }


        //Добавление вопроса с вариантами ответа ✔
        //Добавить проверку на админа
        public IActionResult CreateVopr(PassingTestModel model)
        {
            if (model.createAnswer.TextOtvTest.Replace(" ", "") != "")
            {
                int idthem = model.createAnswer.IdTheme;
                TestAnswer entity1 = new TestAnswer()
                {
                    IDTheme = model.createAnswer.IdTheme,
                    necessarily = model.createAnswer.IsRec,
                    Question = model.createAnswer.TextOtvTest
                };


                this.BD.TestAnswer.Add(entity1);
                this.BD.SaveChanges();
                if (model.createAnswer.Q == null)
                {
                    for (int index = 1; index <= 5; ++index)
                        this.BD.AnswerT.Add(new AnswerT()
                        {
                            Correct = model.createAnswer.IsRec,
                            IDTestAnswer = entity1.ID,
                            NumberOfSelected = 0,
                            Text = index.ToString()
                        });
                }
                else
                {
                    string[] source = model.createAnswer.Q.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    for (int index = 1; index <= source.Count(); ++index)
                        this.BD.AnswerT.Add(new AnswerT()
                        {
                            Correct = model.createAnswer.IsRec,
                            IDTestAnswer = entity1.ID,
                            NumberOfSelected = 0,
                            Text = index.ToString(),
                            TextUser = source[index - 1]
                        });
                }
                this.BD.SaveChanges();

                if (BD.TestSort.Where(x => x.IDtheme == model.createAnswer.IdTheme).Any())
                {
                    var CountSort = BD.TestSort.Where(x => x.IDtheme == model.createAnswer.IdTheme).Count();
                    TestSort NewSort = new TestSort()
                    {
                        IDtheme = model.createAnswer.IdTheme,
                        IDques = entity1.ID,
                        Number = CountSort + 1,
                        Type = "CloseTest"

                    };
                    BD.TestSort.Add(NewSort);
                }
                else
                {
                    TestSort NewSort = new TestSort()
                    {
                        IDtheme = model.createAnswer.IdTheme,
                        IDques = entity1.ID,
                        Number = 1,
                        Type = "CloseTest"

                    };
                    BD.TestSort.Add(NewSort);
                }
                this.BD.SaveChanges();
            }
            return RedirectToAction("ChangingTest", "Home", new { IdTest = model.createAnswer.IdTest });
        }


        //Удаление вопроса из темы ✔
        //Добавить проверку на админа
        public async Task<IActionResult> DeleteQuestion(int IdTest, int IDques, string Type)
        {
            switch (Type)
            {
                case "CloseTest":

                    var itemAnswer = await BD.TestAnswer.Where(x => x.ID == IDques).FirstOrDefaultAsync();
                    foreach (var itemAnswerT in await BD.AnswerT.Where(x => x.IDTestAnswer == itemAnswer.ID).ToListAsync())
                    {
                        BD.AnswerT.Remove(itemAnswerT);
                    }
                    BD.TestAnswer.Remove(itemAnswer);

                    BD.TestSort.Remove(await BD.TestSort.Where(x => x.IDques == IDques && x.Type == Type).FirstOrDefaultAsync());
                    break;

                case "OpenTest":



                    //Удаление открытого вопроса
                    var itemOpen = await BD.TestOpen.Where(x => x.ID == IDques).FirstOrDefaultAsync();

                    foreach (var itemOpenAnswer in await BD.AnswerOpenTest.Where(x => x.IDTestOpen == itemOpen.ID).ToListAsync())
                    {
                        BD.AnswerOpenTest.Remove(itemOpenAnswer);
                    }
                    BD.TestOpen.Remove(itemOpen);

                    BD.TestSort.Remove(await BD.TestSort.Where(x => x.IDques == IDques && x.Type == Type).FirstOrDefaultAsync());

                    break;

                case "TableTest":
                    var itemTable = await BD.TableTest.Where(x => x.ID == IDques).FirstOrDefaultAsync();
                    foreach (var itemTheme in await BD.Theme.Where(x => x.IDTable == itemTable.ID).ToListAsync())
                    {
                        foreach (var itemAnswerTheme in await BD.AnswerTheme.Where(x => x.IDTheme == itemTheme.ID).ToListAsync())
                        {
                            BD.AnswerTheme.Remove(itemAnswerTheme);
                        }
                        BD.Theme.Remove(itemTheme);
                    }
                    foreach (var itemQuestion in await BD.question.Where(x => x.IDTable == itemTable.ID).ToListAsync())
                    {
                        BD.question.Remove(itemQuestion);
                    }
                    BD.TableTest.Remove(itemTable);
                    BD.TestSort.Remove(await BD.TestSort.Where(x => x.IDques == IDques && x.Type == Type).FirstOrDefaultAsync());

                    break;

            }






            await BD.SaveChangesAsync();


            return RedirectToAction("ChangingTest", "Home", new { IdTest = IdTest });
        }

        //Удаление темы теста ✔
        //Добавить проверку на админа
        public async Task<IActionResult> DeleteTheme(int IdTheme, int IdTest)
        {



            //Удаление открытого вопроса
            foreach (var itemOpen in await BD.TestOpen.Where(x => x.IDTheme == IdTheme).ToListAsync())
            {
                foreach (var itemOpenAnswer in await BD.AnswerOpenTest.Where(x => x.IDTestOpen == itemOpen.ID).ToListAsync())
                {
                    BD.AnswerOpenTest.Remove(itemOpenAnswer);
                }
                BD.TestOpen.Remove(itemOpen);
            }

            //Удаление вопроса с выбором ответа
            foreach (var itemAnswer in await BD.TestAnswer.Where(x => x.IDTheme == IdTheme).ToListAsync())
            {
                foreach (var itemAnswerT in await BD.AnswerT.Where(x => x.IDTestAnswer == itemAnswer.ID).ToListAsync())
                {
                    BD.AnswerT.Remove(itemAnswerT);
                }
                BD.TestAnswer.Remove(itemAnswer);
            }

            //удаление табличного вопроса
            foreach (var itemTable in await BD.TableTest.Where(x => x.IDTheme == IdTheme).ToListAsync())
            {
                foreach (var itemTheme in await BD.Theme.Where(x => x.IDTable == itemTable.ID).ToListAsync())
                {
                    foreach (var itemAnswerTheme in await BD.AnswerTheme.Where(x => x.IDTheme == itemTheme.ID).ToListAsync())
                    {
                        BD.AnswerTheme.Remove(itemAnswerTheme);
                    }
                    BD.Theme.Remove(itemTheme);
                }
                foreach (var itemQuestion in await BD.question.Where(x => x.IDTable == itemTable.ID).ToListAsync())
                {
                    BD.question.Remove(itemQuestion);
                }
                BD.TableTest.Remove(itemTable);
            }

            //Удаление Сортировочного листа
            foreach (var itemSort in await BD.TestSort.Where(x => x.IDtheme == IdTheme).ToListAsync())
            {
                BD.TestSort.Remove(itemSort);
            }


            BD.ThemeTest.Remove(await BD.ThemeTest.Where(x => x.ID == IdTheme).FirstOrDefaultAsync());
            await BD.SaveChangesAsync();

            return RedirectToAction("ChangingTest", "Home", new { IdTest = IdTest });
        }

        #endregion


        #region Удаление вариантов ответов (Добавить проверку на админа) ✔

        //Удаление варианта ответа из вопроса с ответами ✔
        //Добавить проверку на админа
        public async Task<IActionResult> DeleteAnswerClose(int IdTest, int IDques, int IdAnswer, string Type)
        {

            BD.AnswerT.Remove(await BD.AnswerT.Where(x => x.ID == IdAnswer).FirstOrDefaultAsync());
            await BD.SaveChangesAsync();
            return RedirectToAction("EditingQuestion", "Home", new { IdTest= IdTest, IDques = IDques, Type= Type });
        }
        
        
        //Удаление темы из таблицы ✔
        //Добавить проверку на админа
        public async Task<IActionResult> DeleteAnswerTable(int IdTest, int IDques, int IdAnswer, string Type)
        {

            BD.Theme.Remove(await BD.Theme.Where(x => x.ID == IdAnswer).FirstOrDefaultAsync());

            foreach (var item in await BD.AnswerTheme.Where(x=>x.IDTheme== IdAnswer).ToListAsync())
            {
                BD.AnswerTheme.Remove(item);
            }
            await BD.SaveChangesAsync();
            return RedirectToAction("EditingQuestion", "Home", new { IdTest = IdTest, IDques = IDques, Type = Type });
        }
        
        
        //Удаление вопроса из таблицы ✔
        //Добавить проверку на админа
        public async Task<IActionResult> DeleteQuesTable(int IdTest, int IDques, int IdAnswer, string Type)
        {

            BD.question.Remove(await BD.question.Where(x => x.ID == IdAnswer).FirstOrDefaultAsync());

            foreach (var item in await BD.AnswerTheme.Where(x => x.IDQuestion == IdAnswer).ToListAsync())
            {
                BD.AnswerTheme.Remove(item);
            }
            await BD.SaveChangesAsync();
            return RedirectToAction("EditingQuestion", "Home", new { IdTest = IdTest, IDques = IDques, Type = Type });
        }
       
        
        //Редактирование таблицы из темы ✔
        //Добавить проверку на админа
        public async Task<IActionResult> EditingQuestionSet(EditingQuestionViewModel model)
        {
            switch (model.Type)
            {
                case "CloseTest":

                    var itemAnswer = await BD.TestAnswer.Where(x => x.ID == model.editingCloseModel.Id).FirstOrDefaultAsync();

                    itemAnswer.Question = model.editingCloseModel.Name;
                    itemAnswer.necessarily = model.editingCloseModel.IsRec;

                    if (model.editingCloseModel.AnsweT !=null)
                    {
                        foreach (var item in model.editingCloseModel.AnsweT)
                        {
                            var Ans =  await BD.AnswerT.Where(x => x.ID == item.Key).FirstOrDefaultAsync();
                            if (item.Value!=null)
                            {
                                Ans.TextUser = item.Value;
                            }

                            
                            
                        }

                        if (model.editingCloseModel.NewQues != null)
                        {
                            string[] source = model.editingCloseModel.NewQues.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                            for (int index = 1; index <= source.Count(); ++index)
                                this.BD.AnswerT.Add(new AnswerT()
                                {
                                    Correct = model.editingCloseModel.IsRec,
                                    IDTestAnswer = model.editingCloseModel.Id,
                                    NumberOfSelected = 0,
                                    TextUser = source[index - 1],
                                    Text = index.ToString()
                                });
                        }
                    }
                    else
                    {

                        if (model.editingCloseModel.NewQues== null)
                        {

                            for (int index = 1; index <= 5; ++index)
                                this.BD.AnswerT.Add(new AnswerT()
                                {
                                    Correct = model.editingCloseModel.IsRec,
                                    IDTestAnswer = model.editingCloseModel.Id,
                                    NumberOfSelected = 0,
                                    Text = index.ToString()
                                });
                        }
                        else
                        {
                            string[] source = model.editingCloseModel.NewQues.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                            for (int index = 1; index <= source.Count(); ++index)
                                this.BD.AnswerT.Add(new AnswerT()
                                {
                                    Correct = model.editingCloseModel.IsRec,
                                    IDTestAnswer = model.editingCloseModel.Id,
                                    NumberOfSelected = 0,
                                    TextUser = source[index-1],
                                    Text = index.ToString()
                                });

                        }
                    }


                    break;

                case "OpenTest":
                    var itemOpen = await BD.TestOpen.Where(x => x.ID == model.editingOpenModel.Id).FirstOrDefaultAsync();


                    itemOpen.Question = model.editingOpenModel.Name;
                    itemOpen.necessarily = model.editingOpenModel.IsRec;

                    break;

                case "TableTest":
                    var itemTable = await BD.TableTest.Where(x => x.ID == model.editingTableModel.Id).FirstOrDefaultAsync();

                    itemTable.Desp = model.editingTableModel.Desc;
                    itemTable.necessarily = model.editingTableModel.IsRec;
                    itemTable.Name = model.editingTableModel.Name;


                    foreach (var item in model.editingTableModel.AnswerList)
                    {
                        var Ans = await BD.Theme.Where(x => x.ID == item.Key).FirstOrDefaultAsync();
                        Ans.Text = item.Value;
                    }

                    foreach (var item in model.editingTableModel.QuesList)
                    {
                        var Ques = await BD.question.Where(x => x.ID == item.Key).FirstOrDefaultAsync();
                        Ques.Text = item.Value; 
                    }


                    if (model.editingTableModel.NewAnswer != null)
                    {
                        string[] strArray1 = model.editingTableModel.NewAnswer.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                        foreach (string str in strArray1)
                        {
                            this.BD.Theme.Add(new Theme()
                            {
                                IDTable = itemTable.ID,
                                Text = str
                            });
                        }

                    }

                    if (model.editingTableModel.NewQues != null)
                    {
                        string[] strArray2 = model.editingTableModel.NewQues.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                        foreach (string str in strArray2)
                        {
                            this.BD.question.Add(new question()
                            {
                                IDTable = itemTable.ID,
                                Text = str
                            });
                        }

                    }

                    

                    

                    break;

            }
            await BD.SaveChangesAsync();

            return RedirectToAction("ChangingTest", "Home", new { IdTest = model.IdTest});
        }
        #endregion


        //Отображение редактирования вопроса из темы ✔
        //Добавить проверку на админа
        public async Task<IActionResult> EditingQuestion(int IdTest, int IDques, string Type)
        {
            EditingQuestionViewModel viewModel = new EditingQuestionViewModel();
            viewModel.Type = Type;
            viewModel.IdTest = IdTest;
            switch (Type)
            {
                case "CloseTest":
                    
                    var itemAnswer = await BD.TestAnswer.Where(x => x.ID == IDques).FirstOrDefaultAsync();
                    viewModel.editingCloseModel = new EditingCloseModel() { Id= itemAnswer.ID, IsRec= itemAnswer.necessarily, Name = itemAnswer.Question };
                    viewModel.editingCloseModel.AnsweT = new List<KeyValuePair<int, string>>();
                    viewModel.editingOpenModel = new EditingOpenModel();


                    foreach (var itemAnswerT in await BD.AnswerT.Where(x => x.IDTestAnswer == itemAnswer.ID).ToListAsync())
                    {
                        if (itemAnswerT.TextUser ==null)
                        {
                            viewModel.editingCloseModel.AnsweT.Add(new KeyValuePair<int, string>(itemAnswerT.ID, itemAnswerT.Text));

                        }
                        else
                        {
                            viewModel.editingCloseModel.AnsweT.Add(new KeyValuePair<int, string>(itemAnswerT.ID, itemAnswerT.TextUser));

                        }
                    }

                    break;

                case "OpenTest":



                    
                    var itemOpen = await BD.TestOpen.Where(x => x.ID == IDques).FirstOrDefaultAsync();
                    viewModel.editingOpenModel = new EditingOpenModel();
                    viewModel.editingOpenModel.Id = itemOpen.ID;
                    viewModel.editingOpenModel.Name = itemOpen.Question;
                    viewModel.editingOpenModel.IsRec = itemOpen.necessarily;


                    break;

                case "TableTest":
                    var itemTable = await BD.TableTest.Where(x => x.ID == IDques).FirstOrDefaultAsync();
                    viewModel.editingTableModel = new EditingTableModel();

                    viewModel.editingTableModel.Id = itemTable.ID;
                    viewModel.editingTableModel.Desc = itemTable.Desp;
                    viewModel.editingTableModel.IsRec = itemTable.necessarily;
                    viewModel.editingTableModel.Name = itemTable.Name;

                    viewModel.editingTableModel.AnswerList = new List<KeyValuePair<int, string>>();
                    foreach (var itemTheme in await BD.Theme.Where(x => x.IDTable == itemTable.ID).ToListAsync())
                    {
                        viewModel.editingTableModel.AnswerList.Add(new KeyValuePair<int, string>(itemTheme.ID, itemTheme.Text));
                    }

                    viewModel.editingTableModel.QuesList = new List<KeyValuePair<int, string>>();
                    foreach (var itemQuestion in await BD.question.Where(x => x.IDTable == itemTable.ID).ToListAsync())
                    {
                        viewModel.editingTableModel.QuesList.Add(new KeyValuePair<int, string>(itemQuestion.ID, itemQuestion.Text));
                    }
                    
                    break;

            }






            await BD.SaveChangesAsync();


            return View(viewModel);
        }


        //Отображение редактирования теста ✔
        //Добавить проверку на админа
        public IActionResult ChangingTest(int IdTest)
        {





            var TestOpis = BD.OpisTheme.Where(x => x.IdTheme == IdTest).FirstOrDefault();
            var Test = BD.TestSistem.Where(x => x.ID == IdTest).FirstOrDefault();
            PassingTestModel passingTest = new PassingTestModel();


            passingTest.createTheme = new CreateTheme();
            passingTest.OpisCreate = new OpisCreate();

            if (TestOpis != null) passingTest.Opisanie = TestOpis.Opis;
            passingTest.passingThemes = new List<PassingThemeModel>();
            passingTest.Name = Test.Name;
            passingTest.ID = Test.ID;
            if (TestOpis != null) passingTest.bytes = TestOpis.ImageBit;
            if (TestOpis != null) passingTest.url = TestOpis.link;

            List<PassingThemeModel> passingTheme = new List<PassingThemeModel>();


            foreach (var itemTheme in BD.ThemeTest.Where(x => x.IDTestSistem == IdTest))
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
                                closePassing.IsRec = close.necessarily;
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
                                openPassing.IsRec = Open.necessarily;
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


        #region Вспомогательные методы
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


        //Генерация случайного пароля ✔
        public string GetPass()
        {
            int[] numArray = new int[16];
            Random random = new Random();
            string pass = "";
            for (int index = 0; index < numArray.Length; ++index)
            {
                numArray[index] = random.Next(33, 125);
                pass += ((char)numArray[index]).ToString();
            }
            return pass;
        }


        #endregion


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
                        if (item2.Id!=0)
                        {
                            var a = BD.AnswerT.Where(x => x.ID == item2.AnswerTSelect).FirstOrDefault();
                            a.NumberOfSelected++;
                        }
                        

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
        //Добавить проверку на доступ к курсу и проврку на (Прошел/Не прошел)
        public async Task<IActionResult> PassingTheTest()
        {


            loginMain = CoockiesChek();
            if (loginMain != null)
            {
                var User = await BD.UserSelectTest.Where(x => x.Login.ToLower() == loginMain.ToLower()).FirstOrDefaultAsync();

                var TestOpis = BD.OpisTheme.Where(x => x.IdTheme == User.TestID).FirstOrDefault();
                var Test = BD.TestSistem.Where(x => x.ID == User.TestID).FirstOrDefault();
                PassingTestModel passingTest = new PassingTestModel();
                if (TestOpis != null) passingTest.Opisanie = TestOpis.Opis;
                passingTest.passingThemes = new List<PassingThemeModel>();
                passingTest.Name = Test.Name;
                passingTest.ID = Test.ID;
                if (TestOpis != null) passingTest.bytes = TestOpis.ImageBit;
                if (TestOpis != null) passingTest.url = TestOpis.link;







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
                                    closePassing.IsRec = close.necessarily;
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
                                    openPassing.IsRec = Open.necessarily;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
