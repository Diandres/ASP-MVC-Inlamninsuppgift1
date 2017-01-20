using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndresBlogg.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
    }
}
//Skall innehålla ett formulär där det går att lägga in nya blogginlägg.
//2. Ett inlägg skall vara en text på max 2000 tecken.
//3. Det skall även ha en rubrik som får vara max 50 tecken.
//4. Det skall alltid registreras vilket datum inlägget skrivs
//5. Varje inlägg ska kopplas till en kategori.
//En kategori är ett sätt att dela in inlägg i olika typer av inlägg. Välj
//själv vilka kategorier som skall finnas i din lösning