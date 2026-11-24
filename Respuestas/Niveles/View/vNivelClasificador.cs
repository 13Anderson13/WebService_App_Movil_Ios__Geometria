namespace WebServiceGeometria.Respuestas.Niveles.View
{
    public class vNivelClasificador
    {
        public int id_detalle { get; set; }
        public int id_nivel { get; set; }
        public int orden { get; set; }
        public string enunciado { get; set; }
        public string icono { get; set; }
        public List<string> opciones_json { get; set; }
        public string respuesta_correcta { get; set; }
    }
}
