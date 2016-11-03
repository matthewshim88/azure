using System;
using System.Collections.Generic;
using Nancy;

namespace NinjaGold
{
    public class NinjaGold: NancyModule
    {

        public NinjaGold()
        {
            Random randNum = new Random();
            Get("/", args =>
            {
                if(Session["gold"] is int)
                {
                    ViewBag.show = true;
                    ViewBag.gold = Session["gold"];
                    ViewBag.actions = Session["actionList"];
                }else{
                    ViewBag.show = true;
                    Session["gold"] = 0;
                    ViewBag.gold = Session["gold"];
                    Session["actionList"] = new List<string>();
                }
                return View["NinjaGold", Session["actionList"]];

            });
            Post("/process_money", args =>
            {
                int currentGold = (int)Session["gold"];
                int goldEarned = 0; 
                if(Request.Form["building"] == "Farm")
                {
                    goldEarned = randNum.Next(10,20);
                    ((List<string>)Session["actionList"]).Add($"Gold Earned from Farm: $ {goldEarned}");
                }else if(Request.Form["building"] == "Cave")
                {
                    goldEarned = randNum.Next(5,10);
                    ((List<string>)Session["actionList"]).Add($"Gold Earned from Cave: $ {goldEarned}");
                }else if(Request.Form["building"]== "House")
                {
                    goldEarned = randNum.Next(2,5);
                    ((List<string>)Session["actionList"]).Add($"Gold Earned from House: $ {goldEarned}");
                }else if(Request.Form["building"] == "Casino")
                {
                    goldEarned = randNum.Next(-50, 50);
                    ((List<string>)Session["actionList"]).Add($"Gold Earned from Casino $ {goldEarned}");
                }
                currentGold += goldEarned;
                Session["gold"] = currentGold;
                return Response.AsRedirect("/");
            });

            Post("/reset", args =>
            {
                Session.DeleteAll();
                return Response.AsRedirect("/");
            });

        }
    }
}