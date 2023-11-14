namespace Syntax_1;

/*
    Done: Printf -> printf(cadena(, Identificador)?);
    Done: Scanf -> scanf(cadena,&Identificador);
    Requerimiento 3: Agregar a la Asignacion +=, -=, *=. /=, %=
                     Ejemplo:
                     Identificador IncrementoTermino Expresion;
                     Identificador IncrementoFactor Expresion;
    Done: Agregar el else optativo al if
    Requerimiento 5: Indicar el número de linea de los errores
*/

public class Language : Syntax
{
    public Language()
    {
    }

    public Language(string name) : base(name)
    {
    }

    //Programa  -> Librerias? Variables? Mai
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
            Match(Types.Identifier);

            if(GetContent() == ",")
            {
                while (GetClassification() == Types.Identifier || GetContent() == ",")
                {
                    Match(",");
                    Match(Types.Identifier);
                }
            }
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
            InstructionBlock();
        }
    }

    //Delete - Lista_identificadores -> identificador (,Lista_identificadores)?
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
        if(GetClassification() == Types.DataTypes)
        {
            Match(Types.DataTypes);
            Match(Types.Identifier);

            while (GetContent() == ",")
            {
                Match(",");
                Match(Types.DataTypes);
                Match(Types.Identifier);
            }
        }
        Match(")");
    }

    //Bloque de instrucciones -> {lista de intrucciones?}
    public void InstructionBlock()
    {
        Match("{");
        if (GetContent() != "}")
        {
            InstructionList();
        }

        Match("}");
    }

    // ListaInstrucciones -> Instruccion ListaInstrucciones?
    private void InstructionList()
    {
        Instructions();
        if (GetContent() != "}")
        {
            InstructionList();
        }
    }

    //Instruccion -> Printf | Scanf | If | While | do while | For | Asignacion
    private void Instructions()
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
        Concatenation();
        Match(")");
        Match(";");
    }

    //Scanf -> scanf(cadena);
    private void Scanf()
    {
        Match("scanf");
        Match("(");
        Match(Types.String);
        Concatenation();
        Match(")");
        Match(";");
    }
    // Concatenation
    private void Concatenation()
    {
        if(GetContent() == ",")
        {
            Match(",");
            Match(Types.Identifier);
        }
    }
    private void Assignment()
    {
    }

    //While -> while(Condicion) bloque de instrucciones | instruccion
    private void While()
    {
        Match("while");
        Match("(");
        Condition();
        Match(")");
        InstructionBlock();
    }

    //Do -> do bloque de instrucciones | intruccion while(Condicion)
    private void Do()
    {
        Match("do");
        InstructionBlock();
        Match("while");
        Match("(");
        Condition();
        Match(")");
        Match(";");
    }

    //Testing - For -> for(Asignacion Condicion; Incremento) Bloque de instruccones | Intruccion
    private void For()
    {
        Match("for");
        Match("(");
        // Under Test
        if(GetClassification() == Types.DataTypes)
        {
            Variable();
        }
        else
        {
            Match(Types.Identifier);
            Match("=");
            Match(Types.Number);
        }
        Match(";");
        Condition();
        Match(";");
        Increase();
        Match(")");
        InstructionBlock();
    }
    // foreach?

    //Incremento -> Identificador ++ | --
    private void Increase()
    {
        if(GetClassification() == Types.Identifier)
        {
            Match(Types.Identifier);
            Match(Types.IncreaseTerm);
        }
        else
        {
            Match(Types.IncreaseTerm);
            Match(Types.Identifier);
        }
    }

    //Condicion -> Expresion operador relacional Expresion
    private void Condition()
    {
        Match(GetClassification() == Types.Identifier ? Types.Identifier : Types.Number);
        Match(Types.RelationalOp);
        Match(GetClassification() == Types.Identifier ? Types.Identifier : Types.Number);
    }

    //If -> if(Condicion) bloque de instrucciones (else bloque de instrucciones)?
    private void If()
    {
        Match("if");
        Match("(");
        Condition();
        Match(")");
        InstructionBlock();
        if(GetContent() == "else")
        {
            Match("else");
            if(GetContent() == "if")
            {
                If();
            }
            else
            {
                InstructionBlock();
            }
        }
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