namespace Syntax_1;

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

            if (GetContent() == ",")
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

    // Parameters
    public void Parameters()
    {
        Match("(");
        if (GetClassification() == Types.DataTypes)
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
            InstructionList();

        Match("}");
    }

    // ListaInstrucciones -> Instruccion ListaInstrucciones?
    private void InstructionList()
    {
        Instructions();

        if (GetContent() != "}")
            InstructionList();
    }

    //Instruccion -> Printf | Scanf | If | While | do while | For | Asignacion
    private void Instructions()
    {
        if (GetContent() == "printf")
            Printf();
        else if (GetContent() == "scanf")
            Scanf();
        else if (GetContent() == "if")
            If();
        else if (GetContent() == "while")
            While();
        else if (GetContent() == "do")
            Do();
        else if (GetContent() == "for")
            For();
        else
            Assignment();
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

    //Concatenation
    private void Concatenation()
    {
        if (GetContent() == ",")
        {
            Match(",");
            Match(Types.Identifier);
        }
    }

    // //Assignment -> Identificador (++ | --) | (= Expresion);
    private void Assignment()
    {
        Match(Types.Identifier);
        
        if (GetClassification() == Types.IncreaseTerm)
        {
            Match(Types.IncreaseTerm);

            if(GetContent() == "(")
                Expression();
        }
        else if (GetClassification() == Types.FactorIn)
        {
            Match(Types.FactorIn);
            Expression();
        }
        else
        {
            Match("=");
            Expression();
        }

        Match(";");
    }

    //While -> while(Condicion) bloque de instrucciones | instruccion
    private void While()
    {
        Match("while");
        Match("(");
        Condition();
        Match(")");

        if (GetContent() == "{")
            InstructionBlock();
        else
            Instructions();
    }

    //Do -> do bloque de instrucciones | intruccion while(Condicion)
    private void Do()
    {
        Match("do");

        if (GetContent() == "{")
            InstructionBlock();
        else
            Instructions();

        Match("while");
        Match("(");
        Condition();
        Match(")");
        Match(";");
    }

    //For -> for(Asignacion Condicion; Incremento) Bloque de instruccones | Intruccion
    private void For()
    {
        Match("for");
        Match("(");

        if (GetClassification() == Types.DataTypes)
            Variable();
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

        if (GetContent() == "{")
            InstructionBlock();
        else
            Instructions();
    }

    //Incremento -> Identificador ++ | --
    private void Increase()
    {
        if (GetClassification() == Types.Identifier)
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
        Expression();
        Match(Types.RelationalOp);
        Expression();
    }

    //If -> if(Condicion) instruccion | bloque de instrucciones
    // (else bloque de instrucciones)?
    private void If()
    {
        Match("if");
        Match("(");
        Condition();
        Match(")");
        if (GetContent() == "{")
            InstructionBlock();
        else
            Instructions();

        if (GetContent() == "else")
        {
            if (GetContent() == "{")
                InstructionBlock();
            else
                Instructions();
        }
    }

    //Expresion -> Termino Mas Termino
    private void Expression()
    {
        Term();
        PlusTerm();
    }

    //MasTermino -> (Operador Termino Termino)?
    private void PlusTerm()
    {
        if (GetClassification() == Types.TermOp)
        {
            Match(Types.TermOp);
            Term();
        }
    }
    //Termino -> Factor Por Factor
    private void Term()
    {
        Factor();
        ByFactor();
    }

    //PorFactor -> (Operador Factor Factor)?
    private void ByFactor()
    {
        if (GetClassification() == Types.FactorOp)
        {
            Match(Types.FactorOp);
            Factor();
        }
    }

    //Factor - numero | identificador | (Expresion)
    private void Factor()
    {
        if (GetClassification() == Types.Number)
            Match(Types.Number);
        else if (GetClassification() == Types.Identifier)
            Match(Types.Identifier);
        else
        {
            Match("(");
            Expression();
            Match(")");
        }
    }
}