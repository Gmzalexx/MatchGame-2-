using System.Diagnostics;

namespace MatchGame;

public partial class GameOff : ContentPage
{
    //Se declara una variable privada llamada stopwatch del tipo Stopwatch.
    //esta se utilizará para medir el tiempo de juego 
    private Stopwatch stopwatch;

    public GameOff()
	{ 
		InitializeComponent();

        SetUpGame();

        //Inicia el temporizador 
        StartTimer();
    }

    private void SetUpGame()
    {
        List<String> animalEmoji = new List<String>()
        //Definimos una lista de emojis de animales duplicados, lo que permite emparejar los emojis.
    {
        "😎","😎",
        "🐯","🐯",
        "🦓","🦓",
        "🙈","🙈",
        "🦊","🦊",
        "🐭","🐭",
        "🙊","🙊",
        "🐻","🐻",
        "🐼","🐼",
    };

        //Iniciamos un generador de números aleatorios llamado random este mezclara la lista de emoticones utilizando
        //el algoritmo de Fisher-Yates para obtener una distribución aleatoria.
        Random random = new Random();
        int n = animalEmoji.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            string value = animalEmoji[k];
            animalEmoji[k] = animalEmoji[n];
            animalEmoji[n] = value;
        }
        //Asignamos emoticones variados a cada botón dentro del Grid seleccionamos un emoticon aleatorio de la lista variada
        //y esta se eliminara de la lista para evitar duplicados.
        foreach (Button view in Grid1.Children)
        {
            int index = random.Next(animalEmoji.Count);
            string nextEmoji = animalEmoji[index];
            view.Text = nextEmoji;
            animalEmoji.RemoveAt(index);
        }
    }
        private void StartTimer()
        {
            //Hacemos una instancia de un nuevo objeto Stopwatch para medir el tiempo.
            stopwatch = new Stopwatch();
            //Iniciamos el cronómetro.
            stopwatch.Start();
            //Iniciamos un temporizador que se ejecutará cada segundo.
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                // Actualizara el tiempo transcurrido en la pantalla cuando se presente el juego 
                lblTiempoTranscurrido.Text = stopwatch.Elapsed.ToString("mm\\:ss");
                //Verificaos si el tiempo transcurrido se ha terminado o pasado el límite de tiempo .
                if (stopwatch.Elapsed.TotalMinutes >= 1) 
                {
                    // Detendra el temporizador si se ha alcanzado el límite de tiempo establecido
                    stopwatch.Stop();
                    // Devolvera un false para indicar que el temporizador se va a detener.
                    return false; 
                }
                // Devolvera un true para indicar que el temporizador debera seguir ejecutándose como se ha programado
                return true; 
            });
        }

        //Declaramos dos variables: ultimoButtonClicked, que almacenará el último botón presionado, y
        //encontrandoMatch, una bandera que indicara si se está buscando un par de botones coincidentes.
        Button ultimoButtonClicked;
        bool encontrandoMatch = false;


        private void Button_Clicked(object sender, EventArgs e)
        {
            //Obtenemos un botón que ha sido clickeado y se le asigna a la variable un 'button'.
            Button button = sender as Button;
            if (encontrandoMatch == false)
            {
                // Si está no esta buscando un par coincidente, ocultara el botón presionado.
                button.IsVisible = false;
                // Asignamos el botón presionado como el último botón clickeado.
                ultimoButtonClicked = button;
                //Indicamos que se está buscando un par coincidente.
                encontrandoMatch = true; 
            }
            else if (button.Text == ultimoButtonClicked.Text)
            {
                //El botón que tenemos tiene el mismo texto que el último botón presionado, este ocultara el botón actual.
                button.IsVisible = false;
                //Reinicia la búsqueda de un par coincidente.
                encontrandoMatch = false; 
            }
            else
            {
                // Si los botones  que tenemos no coinciden, nos mostrara nuevamente el último botón que presionamos
                ultimoButtonClicked.IsVisible = true;
                //Este reinicia la búsqueda de un par coincidente.
                encontrandoMatch = false; 
            }
        }

    private void ResetGame(object sender, EventArgs e)
    {
        //Detiene el temporizador si está en medio del juego
        stopwatch.Stop();

        //Reinicia el temporizador tocando el boton
        stopwatch.Reset();

        //Reinicia el tiempo transcurrido en la pantalla del juego
        lblTiempoTranscurrido.Text = "00:00";

        //Vuelve a mostrar todos los botones nuevamente desde su inicio
        foreach (Button view in Grid1.Children)
        {
            view.IsVisible = true;
        }

        //Vuelve a mezclar los emoticones para luego mostrarlas en los botones
        SetUpGame();

        // Reinicia el juego dependiendo en el estado que vaya
        ultimoButtonClicked = null;
        encontrandoMatch = false;

        //Vuelve a iniciar el temporizador
        StartTimer();
    }


}


