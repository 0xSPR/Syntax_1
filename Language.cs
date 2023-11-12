namespace Syntax_1;

public class Language : Syntax
{
    public Language()
    {
    }

    public Language(string name) : base(name)
    {
    }

    //Programa  -> Librerias? Variables? Main
    public void Program()
    {
        Library();
        Variable();
        Void();
    }

    // Librerias -> (#include<Identificador(.h)?>)?
    public void Library()
    {
        if (GetContent() == "#")
        {
            Match("#");
            Match("include");
            Match("<");
            Match(Types.Identifier);
            if (GetContent() == ".")
            {
                Match(".");
                Match("h");
            }

            Match(">");

            Library();
        }
    }

    //Variables -> tipo_dato Lista_identificadores = ; Variables?
    public void Variable()
    {
        if (GetClassification() == Types.DataTypes)
        {
            string type = GetContent();

            Match(Types.DataTypes);
            ListIdentifier();

            if (GetContent() == "=")
            {
                Match("=");

                switch (type)
                {
                    case "int":
                    case "float":
                    case "double":
                    case "byte":
                    case "decimal":
                    case "short":
                        Match(Types.Number);
                        break;
                    case "char":
                    case "bool":
                    case "string":
                        Match(Types.String);
                        break;
                }
            }

            Match(";");

            Variable();
        }
    }

    //public void
    public void Void()
    {
        if (GetContent() == "void")
        {
            Match("void");
            Match("main");
            Parameters();
        }
    }

    //Lista_identificadores -> identificador (,Lista_identificadores)?
    private void ListIdentifier()
    {
        Match(Types.Identifier);
        if (GetContent() == ",")
        {
            Match(",");
            ListIdentifier();
        }
    }

    // Parameters
    public void Parameters()
    {
        Match("(");
        if (GetClassification() == Types.DataTypes)
        {
            //Variable();
        }

        Match(")");
    }

    //Bloque de instrucciones -> {lista de intrucciones?}
    public void InstructionBlock()
    {
        Match("{");

        Match("}");
    }

    //bloqueInstrucciones -> { listaIntrucciones? }
    private void bloqueInstrucciones()
    {
        Match("{");
        if (GetContent() != "}")
        {
            ListaInstrucciones();
        }

        Match("}");
    }

    //ListaInstrucciones -> Instruccion ListaInstrucciones?
    private void ListaInstrucciones()
    {
        Instruccion();
        if (GetContent() != "}")
        {
            ListaInstrucciones();
        }
    }

    //Instruccion -> Printf | Scanf | If | While | do while | For | Asignacion
    private void Instruccion()
    {
        if (GetContent() == "printf")
        {
            Printf();
        }
        else if (GetContent() == "scanf")
        {
            Scanf();
        }
        else if (GetContent() == "if")
        {
            If();
        }
        else if (GetContent() == "while")
        {
            While();
        }
        else if (GetContent() == "do")
        {
            Do();
        }
        else if (GetContent() == "for")
        {
            For();
        }
        else
        {
            Assignment();
        }
    }

    //Printf -> printf(cadena);
    private void Printf()
    {
        Match("printf");
        Match("(");
        Match(Types.String);
        Match(")");
        Match(";");
    }

    //Scanf -> scanf(cadena);
    private void Scanf()
    {
        Match("scanf");
        Match("(");
        Match(Types.String);
        Match(")");
        Match(";");
    }

    private void Assignment()
    {
    }

    //While -> while(Condicion) bloque de instrucciones | instruccion
    private void While()
    {
    }

    //Do -> do bloque de instrucciones | intruccion while(Condicion)
    private void Do()
    {
    }

    //For -> for(Asignacion Condicion; Incremento) Bloque de instruccones | Intruccion 
    private void For()
    {
    }

    //Incremento -> Identificador ++ | --
    private void Increase()
    {
    }

    //Condicion -> Expresion operador relacional Expresion
    private void Condition()
    {
    }

    //If -> if(Condicion) bloque de instrucciones (else bloque de instrucciones)?
    private void If()
    {
    }

    //Printf -> printf(cadena);
    private void PrintF(string sstring)
    {
    }

    //Scanf -> scanf(cadena);
    private void ScanF(string sstring)
    {
    }

    //Expresion -> Termino MasTermino
    private void Expression()
    {
    }

    //MasTermino -> (OperadorTermino Termino)?
    private void PlusTerm()
    {
    }

    //Termino -> Factor PorFactor
    private void Term()
    {
    }

    //PorFactor -> (OperadorFactor Factor)?
    private void ByFactor()
    {
    }

    //Factor -> numero | identificador | (Expresion)
    private void Factor()
    {
    }
}