namespace WebServiceGeometria.Respuestas.MatchShadow.View
{
    public class vNivel_MatchShadow
    {
        public int id_nivel { get; set; }
        public int numero_nivel { get; set; }
        public int id_minijuego { get; set; }
        public string titulo_nivel { get; set; } = string.Empty;
        public string dificultad { get; set; } = string.Empty;
        public bool estado { get; set; }
    }
}
