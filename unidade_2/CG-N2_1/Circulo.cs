
#define CG_Debug
#define CG_OpenGL
// #define CG_DirectX

using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Circulo : ObjetoGeometria
    {
        public Circulo(
            char rotulo, 
            Objeto paiRef, 
            Ponto4D ptoCentral,
            double raio,
            int qtdPontos
             ) : base(rotulo, paiRef)
        {
            // Distancia entre os pontos
            double distanciaPontos = 360 / qtdPontos;
            double pontoAnterior = 0; 
            // calcular pontos
            base.PontosAdicionar(ptoCentral);
            for (int i = 0; i < qtdPontos; i++)
            {
                base.PontosAdicionar(Matematica.GerarPtosCirculo(pontoAnterior, raio));
                pontoAnterior += distanciaPontos;
            };
        }

        protected override void DesenharObjeto()
        {
#if CG_OpenGL && !CG_DirectX
            GL.Begin(base.PrimitivaTipo);
            foreach (Ponto4D pto in pontosLista)
            {
                GL.Vertex2(pto.X, pto.Y);
            }
            GL.End();
#elif CG_DirectX && !CG_OpenGL
    Console.WriteLine(" .. Coloque aqui o seu código em DirectX");
#elif (CG_DirectX && CG_OpenGL) || (!CG_DirectX && !CG_OpenGL)
    Console.WriteLine(" .. ERRO de Render - escolha OpenGL ou DirectX !!");
#endif
        }

        //TODO: melhorar para exibir não só a lista de pontos (geometria), mas também a topologia ... poderia ser listado estilo OBJ da Wavefrom
#if CG_Debug
        public override string ToString()
        {
            string retorno;
            retorno = "__ Objeto Circulo: " + base.rotulo + "\n";
            for (var i = 0; i < pontosLista.Count; i++)
            {
                retorno += "P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]" + "\n";
            }
            return (retorno);
        }
#endif

    }
}