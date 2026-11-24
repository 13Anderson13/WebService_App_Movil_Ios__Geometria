using System.ComponentModel;

namespace WebServiceGeometria.Respuestas.Niveles.View
{
    public class vPerimetrovsArea
    {
        public int id_detalle { get; set; }
        public int id_nivel { get; set; }
        public int orden { get; set; }
        public int base_disponible { get; set;}
        public int altura { get; set; }
        public string modo { get; set; }
        public List<int> opciones_json { get; set; }
        public int respuesta_correcta { get; set; }
    }
}
