using Microsoft.VisualStudio.TestTools.UnitTesting;
using MBD.Controller.Comparator.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MBD;
using MBD.Model;

namespace Comparator.Tests
{
    [TestClass()]
    public class WordsSequenceInSentenceTests
    {
        [TestMethod()]
        public void testTheSameTwoSentenceCoW3()
        {
            IComparator comparator = new WordsSequenceInSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "to jest test.";
            input.file2 = "to jest test.";
            input.countOfWord = 3;

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(1.8, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testTheSameTwoSentenceCoW2()
        {
            IComparator comparator = new WordsSequenceInSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "to jest test.";
            input.file2 = "to jest test.";
            input.countOfWord = 2;

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(1.5, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testTheSameTwoSentenceCoW4()
        {
            IComparator comparator = new WordsSequenceInSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "to jest tylko test.";
            input.file2 = "to jest tylko test.";
            input.countOfWord = 4;

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(2, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testDuplicateSentence()
        {
            IComparator comparator = new WordsSequenceInSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "to jest tylko test.to jest tylko test";
            input.file2 = "to jest tylko test.";
            input.countOfWord = 2;

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(1.5, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testOneDiffrentSentence()
        {
            IComparator comparator = new WordsSequenceInSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "filename1";
            input.filename2 = "filename2";
            input.file1 = "to jest tylko test.to jest tylko test";
            input.file2 = "to jest tylko test.to nie jest tylko test";
            input.countOfWord = 2;

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(0.77, result.score);
            Assert.AreEqual(1.5, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testBigText()
        {
            IComparator comparator = new WordsSequenceInSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "Loca Con Su Tiguere";
            input.filename2 = "Loca ";
            input.file1 = "Ella ta por mi y por ti borro y eso que tu tiene to y yo ni un kiki.Ella ta por mi y por ti borro y eso que tu tiene to y yo ni un kiki..Tu rueda en un BM to la vaina con de to,.Vive en un cason con picina en el millon,.Come de lo bueno, viste de lo caro, joven empresario que fuma buen abano,.Mi casa e de sin y me mojo cuando llueve, si me tiro pa lomina me quedo en el nueve,.Luchi con el billa y mafu cuando empeno, manlleo si e que jayo y si no hay toy feo,.Tu apuesta a la carrera y practica al polo, yo apuesto al ma jodio y siempre ando en bolo,.Tu rueda bajo aire, yo bajo el solazo, tu bebe perinon y yo bebo agua en jarro,..Ella e loca con su tiguere, loca loca loca.ella e loca con su tiguere, tu suave.Ella e loca con su tiguere, loca loca loca.ella e loca con su tiguere, me tiene sultio..Ella ta por mi y por ti borro y eso que tu tiene to y yo ni un kiki.Ella ta por mi y por ti borro y eso que tu tiene to y yo ni un kiki..Tu la saca a casa e campo yo la llevo a bocachica,.Tu la baja a cabare yo la etrallo en la matica..Tu le rueda a tony y romo, y le gata un billeton, yo la invito a villa mella y la jalto e chicharron..Ella e loca con su tiguere, loca loca loca.ella e loca con su tiguere, con su tiguere..Ella ta por mi y por ti borro y eso que tu tiene to y yo ni un kiki..Ella ta pol mi y pol ti borro.....musica............... tu ta chari con mily..Ella ta por mi y por ti borro y eso que tu tiene to y yo ni un kiki.Ella ta por mi y por ti borro y eso que tu tiene to y yo ni un kiki..Tu viaja pa lo USA y te da bueno Resort, yo viajo si pa Herrera y me doy mi chapuson.Tu tiene to mangao si embargo te dien borra, el nueve ta por mi porque tengo pila e cotorra..Ella e loca con su tiguere, loca loca loca.ella e loca con su tiguere, loca con su tiguere..Ella ta por mi y por ti borro y eso que tu tiene to y yo ni un kiki.Ella ta por mi y por ti borro y eso que tu tiene to y yo ni un kiki";
            input.file2 = "Loca (Loca).No te pongas bruto (Loca).Que te la bebe’.Dance or Die (Loca).Ra-ta-ta..Ella se hace la bruta pa’ cotizar.Cinco minutitos de su saldo del celu contigo.Te cotorrea el oído pa’ tenerte en alta.Ella muere por ti pero por mi es que matas...Sigo tranquila como una paloma de e’quina.Mientras ella se pasa en su BM.Mira yo de aquí no me voy, se que está está por mí.Y ninguna va poder quitármelo de un tirón..Yo soy loca con mi tigre Loca, Loca, Loca.Soy loca con mi tigre Loca, Loca, Loca.Soy loca con mi tigre (Loca, Loca, Loca).Soy Loca con mi tigre (Lloca, Loca, Loca)..El está por mí.Y por ti borró (borró).Y eso que tú tienes to’.Y yo ni un Kikí..El está por mí.Y por ti borró (borró).Y eso que tú tienes to’.Y yo ni un Kikí..Mientras ella te complace con todos tus caprichos.Yo te llevo al malecón por un caminito.Me dicen que tu novia anda con un rifle.Porque te vio bailando mambo pa’ mi ¿Qué no lo permite?..Yo no tengo la culpa de que tú te enamore’.Mientras él te compra flores yo compro condo’ (whooo)..Yo soy loca con mi tigre.Cuando amarro ya no he mira eso es lo que dicen..Yo soy loca con mi tigre, Loca, Loca, Loca.Soy loca con mi tigre, Loca, Loca, Loca.Soy loca con mi tigre (Loca, Loca, Loca).Soy loca con mi tigre.Dios mio! (ah, ah)..Se colán lo ra-ta-ta.No te ponga’ bruto.Que te la bebe Loca (loca) loca...El está por mi.Y por ti borró (borró).Y eso que tu tienes to’.Y yo ni un Kikí..Yo soy loca con mi tigre, Loca, Loca, Loca.Soy loca con mi tigre, Loca, Loca, Loca.La loca, la loca, la loca..Loca, Loca, Loca...";
            input.countOfWord = 2;

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(0.02, result.score);
            Assert.AreEqual(1.5, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testDiffrent()
        {
            IComparator comparator = new WordsSequenceInSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "Loca Con Su Tiguere";
            input.filename2 = "Loca ";
            input.file1 = "ala ma kota.";
            input.file2 = "kot ma ale.";
            input.countOfWord = 2;

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(0, result.score);
            Assert.AreEqual(1.5, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }

        [TestMethod()]
        public void testNullCheck()
        {
            IComparator comparator = new WordsSequenceInSentenceComparator();
            ComparationInput input = new ComparationInput();
            input.filename1 = "Loca Con Su Tiguere";
            input.filename2 = "Loca ";

            ComparationResult result = comparator.compare(input);

            Assert.AreEqual(1, result.score);
            Assert.AreEqual(0, result.weigth);
            Assert.AreEqual(input.filename1, result.filename1);
            Assert.AreEqual(input.filename2, result.filename2);
        }
    }
}