using CrazyMusicians.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrazyMusiciansController : ControllerBase
    {
        static List<Musicians> _musicians = new List<Musicians>()
        {
            new Musicians {Id=1,FullName="Ahmet Çalgı",Title="Famous Instrument Player",FunniestProperty="He always plays the wrong notes, but he's very fun." },
            new Musicians {Id=2,FullName="Zeynep Melodi",Title="Famous Instrument Player",FunniestProperty="Her songs are often misunderstood, but she's very popular." },
            new Musicians {Id=3,FullName="Cemil Akor",Title="Crazy Chordist",FunniestProperty="He frequently changes chords, but he's surprisingly talented." },
            new Musicians {Id=4,FullName="Fatma Nota",Title="Surprise Note Producer",FunniestProperty="She constantly prepares surprises while producing notes." },
            new Musicians {Id=5,FullName="Hasan Ritim",Title="Rhythm Monster",FunniestProperty="He plays every rhythm in his own way, it's offbeat but funny." },
            new Musicians {Id=6,FullName="Elif Armoni",Title="Harmony Master",FunniestProperty="She sometimes plays her harmonies wrong, but she's very creative." },
            new Musicians {Id=7,FullName="Ali Perde",Title="Curtain Specialist",FunniestProperty="Her perdeyi farklı şekilde çalar, her zaman sürprizlidir." },
            new Musicians {Id=8,FullName="Ayşe Rezonans",Title="Resonance Expert",FunniestProperty="She's an expert in resonance, but sometimes it gets too noisy." },
            new Musicians {Id=9,FullName="Murat Ton",Title="Tone Enthusiast",FunniestProperty="The differences in his tones are sometimes funny, but very interesting." },
            new Musicians {Id=10,FullName="Selin Akor",Title="Chord Magician",FunniestProperty="When she changes chords, she sometimes creates a magical atmosphere." }

        };

        [HttpGet]
        public ActionResult<IEnumerable<Musicians>> GetAll()
        {

            return Ok(_musicians);

        }


        [HttpGet("({id})")]
        public ActionResult<IEnumerable<Musicians>> GetById(int id)
        {
            var musicians = _musicians.FirstOrDefault(x => x.Id == id);
            if (musicians is null)
            {
                return NotFound($"Musician with the ID {id} could not be found");
            }

            return Ok(musicians);
        }

        [HttpPost]

        public IActionResult CreateNewMusicians([FromBody] Musicians newMusicians)
        {
            var maxId = _musicians.Max(x => x.Id) + 1;

            newMusicians.Id = maxId;

            _musicians.Add(newMusicians);
            return Ok(newMusicians);


        }

        [HttpPatch("({id})")]

        public IActionResult PatchById(int id, string newTitle, [FromBody] JsonPatchDocument<Musicians> patchDocument)
        {
            var musician = _musicians.FirstOrDefault(x => x.Id == id);

            if (musician is null)
            {
                return NotFound($"Musician with the ID {id} could not be found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            musician.Title = newTitle;
            patchDocument.ApplyTo(musician);
            return Ok(newTitle);
        }

        [HttpPut("(update{id:int:min(1)})")]

        public IActionResult UpdateById(int id, string musicianFullName)
        {
            var newMusicians = _musicians.FirstOrDefault(x => x.Id == id);

            if (newMusicians is null)
            {
                return NotFound("Musician with the ID {id} could not be found");
            }



            newMusicians.FullName = musicianFullName;

            return Ok(newMusicians);


        }

        [HttpDelete("(delete/{id:int:min(1)})")]

        public IActionResult DeleteById(int id)
        {
            var deleteMusician=_musicians.FirstOrDefault(x=>x.Id == id);

            if(deleteMusician is null)
            {
                return NotFound($"Musician with the ID {id} could not be found");
            }

            _musicians.Remove(deleteMusician);
            return NoContent();
           
        }




    }
}
