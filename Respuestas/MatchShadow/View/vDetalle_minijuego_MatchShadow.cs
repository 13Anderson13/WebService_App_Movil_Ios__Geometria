namespace WebServiceGeometria.Respuestas.MatchShadow.View
{
    public class vDetalle_minijuego_MatchShadow
    {
        public int id_detalle { get; set; }
        public int id_nivel { get; set; }
        public int numero_nivel { get; set; }
        public string nombre_figura { get; set; } = string.Empty;
        public string icono { get; set; } = string.Empty;
        public double coordenada_sombra_x { get; set; }
        public double coordenada_sombra_y { get; set; }
        public double coordenada_ficha_x { get; set; }
        public double coordenada_ficha_y { get; set; }
    }
}
