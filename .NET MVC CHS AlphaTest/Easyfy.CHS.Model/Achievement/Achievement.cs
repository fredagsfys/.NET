using Easyfy.CHS.Model.Achievement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easyfy.CHS.Model.Achivement {
  internal class Achievement {
    public string Title { get; set; }
    public AchieveType Type { get; set; }
    public DateTime Date { get; set; }
    public int Points { get; set; }
    public List<Requirement> Requirements { get; set; }
  }

  // Klara en wod på en viss tid eller reps
  // Fran under 4 minuter

  // Köra alla Girls woddar

  // Klara alla nämnda woddar ex. Girls, Heros (Fight gone bad)

  // Ha gjort 1000 pull-ups

  // Ha gjort 10000 Pull-ups

  // Total wod tid 10 h

  // Till hör ett gym

  // Varit medlem på CHF i 1 år

  // Varit med på CrossFit Games

  // Klarat av 100 kg i stöt

  // Klarat 50 pull-ups unbroken

  // Få ett resultat som är 10% bättre än föregående resultat

  // Skrivit 100 kommentarer

  // Har fått rank General

  // Första fran som Överste
  // Ha gjort fran som överste

  // Gymnastikawards
  // Klarar av 10 unbrokoen pistol
  // gå på händer 10 meter
  // 1 musle-up

  // Jämhetsaward
  // Det skall vara lika mellan Metabolic, Gymnastic, Weightlift, som mest 5 % skillnad
  // Måste ha gjort 50 wods.

  // Har du gjort mer än 200 squat eller 200 armhävningar på en wod.

  // Har man gjort 10 parwoddar får man denna award

  // Varje parwod har annan poäng än vanlig wod.

  // Poäng/awards för att ha korrekt information på sin profil och anslutit facebook, twitter, instagram

  // Awards för hur många som följer dig. 

  internal class Requirement {
    public bool Complete { get; set; }
    public string Description { get; set; }

    public TimeSpan Time { get; set; }
    public double Reps { get; set; }
    public double Weight { get; set; }

    public int GeneralInt { get; set; }
    public string GeneralString { get; set; }
    public double GeneralDouble { get; set; }

  }
}
