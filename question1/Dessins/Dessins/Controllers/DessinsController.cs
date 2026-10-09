using Dessins.Events;
using Microsoft.AspNetCore.Mvc;

namespace Dessins.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DessinsController : ControllerBase
    {
        [HttpGet]
        // Rien à modifier ici, juste un exemple de dessin très simple
        public ActionResult GetDrawing1()
        {
            var drawSquare = new DrawSquare(2, 2);
            
            return Ok(drawSquare);
        }

        // TODO: Il faut ajouter une nouvelle action pour dessiner la séquence mentionnée dans l'énoncé
        [HttpGet]
        public ActionResult GetDrawing2()
        {
            var chageColor = new ChangeColor("blue");

            var drawCircle = new DrawCircle(1, 1);

            var waiting = new Wait(3);

            var chageColor2 = new ChangeColor("red");

            var drawSquare = new DrawSquare(0, 2);

            var drawSquare2 = new DrawSquare(2, 2);

            var waiting2 = new Wait(1);

            var chageColor3 = new ChangeColor("yellow");

            var drawStar = new DrawStar(1, 3, 20);

            chageColor.DrawingEvents = new List<DrawingEvent>() { drawCircle, waiting, chageColor2, drawSquare, drawSquare2, waiting2, chageColor3, drawStar };


            //chageColor.DrawingEvents.AddRange(drawCircle, waiting, chageColor2, drawSquare, drawSquare2, waiting2, chageColor3, drawStar);

            return Ok(chageColor);

        }
    }
}
