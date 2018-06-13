using System;
using System.Collections.Generic;
using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Test;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        //#####################
        //#  Gebruiker Tests  #
        //#####################

        [TestMethod]
        public void GebruikersnaamCheck()
        {
            Assert.IsFalse(GebruikerLogic.GebruikersnaamCheck("Ditiseentest")); //Bevat geen cijfer
            Assert.IsFalse(GebruikerLogic.GebruikersnaamCheck("ditiseentest1")); //Bevat geen hoofdletter
            Assert.IsFalse(GebruikerLogic.GebruikersnaamCheck("Test1")); //Te kort
            Assert.IsFalse(GebruikerLogic.GebruikersnaamCheck("Ditisnogmaar1test")); //Te lang
            Assert.IsFalse(GebruikerLogic.GebruikersnaamCheck("Gebruiker12#")); //Bevat een speciaal teken


            Assert.IsTrue(GebruikerLogic.GebruikersnaamCheck("Gebruiker1"));
            Assert.IsTrue(GebruikerLogic.GebruikersnaamCheck("G3Bruiker"));
            Assert.IsTrue(GebruikerLogic.GebruikersnaamCheck("Gebruiker12"));
        }

        [TestMethod()]
        public void WachtwoordCheckTest()
        {
            Assert.IsFalse(GebruikerLogic.WachtwoordCheck("Ditiseentest")); //Bevat geen cijfer
            Assert.IsFalse(GebruikerLogic.WachtwoordCheck("ditiseentest1")); //Bevat geen hoofdletter
            Assert.IsFalse(GebruikerLogic.WachtwoordCheck("Test1")); //Te kort
            Assert.IsFalse(GebruikerLogic.WachtwoordCheck("Ditisnogmaar1test")); //Te lang
            Assert.IsFalse(GebruikerLogic.WachtwoordCheck("Wachtwoord1")); //Bevat geen speciale teken

            Assert.IsTrue(GebruikerLogic.WachtwoordCheck("D!tis1goede"));
            Assert.IsTrue(GebruikerLogic.WachtwoordCheck("D3ze()ok"));
        }

        [TestMethod]
        public void NaamCheckTest()
        {
            Assert.IsFalse(GebruikerLogic.NaamCheck("Naam")); //Geen achternaam
            Assert.IsFalse(GebruikerLogic.NaamCheck("Henk 1")); //Bevat een cijfer
            Assert.IsFalse(GebruikerLogic.NaamCheck("Naam Achtern4am"));
            Assert.IsFalse(GebruikerLogic.NaamCheck("NAAM Achternaam"));

            Assert.IsTrue(GebruikerLogic.NaamCheck("Naam Achternaam"));
            Assert.IsTrue(GebruikerLogic.NaamCheck("Naam Doopnaam Achternaam"));
        }

        [TestMethod]
        public void StraatCheck()
        {
            Assert.IsFalse(GebruikerLogic.StraatCheck("straat"));
            Assert.IsFalse(GebruikerLogic.StraatCheck("straat Straat"));
            Assert.IsFalse(GebruikerLogic.StraatCheck("straat 1"));

            Assert.IsTrue(GebruikerLogic.StraatCheck("Straat"));
            Assert.IsTrue(GebruikerLogic.StraatCheck("Straat Naam"));
        }

        [TestMethod]
        public void HuisnummerCheck()
        {
            Assert.IsFalse(GebruikerLogic.HuisnummerCheck("a1"));
            Assert.IsFalse(GebruikerLogic.HuisnummerCheck("a"));
            Assert.IsFalse(GebruikerLogic.HuisnummerCheck("11abc"));

            Assert.IsTrue(GebruikerLogic.HuisnummerCheck("11ab"));
            Assert.IsTrue(GebruikerLogic.HuisnummerCheck("11b"));
            Assert.IsTrue(GebruikerLogic.HuisnummerCheck("1111"));
        }

        [TestMethod]
        public void WoonplaatsCheck()
        {
            Assert.IsFalse(GebruikerLogic.WoonplaatsCheck("woonplaats"));
            Assert.IsFalse(GebruikerLogic.WoonplaatsCheck("woon plaats"));
            Assert.IsFalse(GebruikerLogic.WoonplaatsCheck("woonplaats 1"));

            Assert.IsTrue(GebruikerLogic.WoonplaatsCheck("Woonplaats"));
            Assert.IsTrue(GebruikerLogic.WoonplaatsCheck("Woon Plaats"));
        }

        [TestMethod]
        public void PostcodeCheck()
        {
            Assert.IsFalse(GebruikerLogic.PostcodeCheck("123ab"));
            Assert.IsFalse(GebruikerLogic.PostcodeCheck("ab1234"));
            Assert.IsFalse(GebruikerLogic.PostcodeCheck("1234a"));

            Assert.IsTrue(GebruikerLogic.PostcodeCheck("1234ab"));
            Assert.IsTrue(GebruikerLogic.PostcodeCheck("9024BA"));
            Assert.IsTrue(GebruikerLogic.PostcodeCheck("3421cd"));
        }
        
        //Registreren en inloggen
        [TestMethod]
        public void RegisterLogInCheck()
        {
            Gebruiker gebruiker = new Gebruiker(1, "G3Bruiker", "D!tis1goede", "een@mail.com", "Naam Achternaam", "Straat", "22", "2910OQ", "Woonplaats", false);
            GebruikerTestContext context = new GebruikerTestContext();

            context.VulUnitTestGebLijst();
            Assert.IsTrue(context.UnitTestAccountCheck(gebruiker));
            context.VoegTestGebruikerToe(gebruiker);

            Assert.IsTrue(context.UnitTestLoginCheck(gebruiker));
        }

        //#################
        //#  Feest Tests  #
        //#################

        [TestMethod]
        public void DatumCheckFalse()
        {
            FeestTestContext context = new FeestTestContext();
            context.VulUnitTestFeestLijst();
            Assert.IsFalse(context.DatumCheck(DateTime.Now.AddDays(9), DateTime.Now.AddDays(11)));
        }

        [TestMethod]
        public void DatumCheckTrue()
        {
            FeestTestContext context = new FeestTestContext();
            context.VulUnitTestFeestLijst();
            Assert.IsTrue(context.DatumCheck(DateTime.Now, DateTime.Now.AddDays(1)));
        }
    }
}
