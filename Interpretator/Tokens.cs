using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static Interpretator.BrowserF;
using static Interpretator.Form_interpretator;

namespace Interpretator
{
   
    
    internal class Tokens
    {
        public static RichTextBox outputTextBox;//вивід
        public static  RichTextBox ErrorsTextBox;//вивід
        public static RichTextBox Info_RTB;
        public static DataGridView Grid;

        internal class VarInfo//інформація про змінну
        { 
            internal string Type;
            internal string Value;
            internal Object Var;
            public VarInfo(string type,dynamic value)
            {
                Type = type;
                Value = value;
            }
        } 
        
        internal readonly List<string> Operations; //оператори
        internal readonly List<string> Operators; //цикли і умови
        
        private Trees MyTree;
        //constructors
        public Tokens(RichTextBox outputBox, RichTextBox errors, DataGridView grid,RichTextBox INFO)
        {
            outputTextBox = outputBox;
            ErrorsTextBox = errors;
            Info_RTB = INFO;
            Grid = grid;
            Operations = new List<string> {":=","+++", "---", ">","<","+","=","-","*","/"};
            Operators = new List<string> {"checkIf","otherwise","cycle"};
            MyTree = new Trees(outputTextBox, ErrorsTextBox,Grid,Info_RTB);
        }
        public enum Types // типи токенів
        {
            Braces,         //{}
            Method,         //____()
            Parentheses,    //()
            Brackets,       //[]
            Quotes,         //" "
            Semicolon,      //;
            Type,           //___ varName
            Var,            //type ___
            Operation,      //=+-
            Dot,            //.
            Comma,          // ,
            Operator,       //if,else,while
            Value ,         //varName = ___
        }
        internal struct Token
        {
            public readonly string value;
            public readonly Types type;
            //constructor
            public Token(dynamic Value,Types Type, RichTextBox output,bool displ)
            {
                value = Value;
                type = Type;
                if(output!=null && displ)
               this.display(output);
            }
            public override string ToString()
            {
                return  "<" + value.ToString() +": "+ type+ ">";
            }

            public void display(RichTextBox ouTextBox)
            {
                ouTextBox.Text += " "+this.ToString()+" ";
            }
        }
        void ErrorHandler(string error)
        {
            ErrorsTextBox.Text += "\n   " + error;
        }
        //-----------------------Parsing--------------------------------
        List<Token> tokens = new List<Token>();//список усіх токенів
        internal bool ParseTokens(string Code, bool display_out,bool inRecursion,bool Body)
        {
            try {
			bool performed = false;
            int CheckIfClosed = 0; //індекс } тіла checkIf
            bool ResultPredicate = false;//результат умови checkIf(___)
            while (Code.Length > 0)//допоки є текст
            {
                var code_length = Code.Length;
                var current_char = Code.ElementAt(0);
                bool next_bracket = false, again = false;
                switch (current_char)
                {
                    case ' ': Code = Code.Remove(0, 1); continue;
                    case '\n':
							{
								Code = Code.Remove(0, 1);
								outputTextBox.Text += '\n';
							}
							continue;
                    case '}':
                        tokens.Add(new Token(Code.ElementAt(0).ToString(CultureInfo.InvariantCulture), Types.Braces, outputTextBox, display_out));
                        Code = Code.Remove(0, 1); continue;
                    case '{':
                        tokens.Add(new Token(Code.ElementAt(0).ToString(CultureInfo.InvariantCulture), Types.Braces, outputTextBox, display_out));
                        Code = Code.Remove(0, 1); continue;
                    case '.':
                        tokens.Add(new Token(Code.ElementAt(0).ToString(CultureInfo.InvariantCulture), Types.Dot, outputTextBox, display_out));
                        Code = Code.Remove(0, 1); continue;
                    case ',':
                        tokens.Add(new Token(Code.ElementAt(0).ToString(CultureInfo.InvariantCulture), Types.Comma, outputTextBox, display_out));
                        Code = Code.Remove(0, 1); continue;
                    case '(':
                        int index_close_parenthesis = 0, x;
                            var parameters = "";
							if (Code.Length > 1)
								if (!Code.ElementAt(1).Equals(')'))
								{
									int opened_parenthesis = 1, opened = 0, closed = 0;
									while (!Code.ElementAt(closed).Equals(")") && opened_parenthesis != 0)
									{
										if (Code.Length > closed)
										{
											if (opened_parenthesis > 0)
											{
												if (!Code.ElementAt(closed + 1).Equals(')'))
													closed = Code.IndexOf(")", closed + 1);
												else closed++;
												opened_parenthesis--;
											}
											else break;
											if (!Code.ElementAt(opened + 1).Equals('('))
												opened = Code.IndexOf("(", opened + 1);
											else opened++;
											if (opened < closed && opened != -1)
											{ opened_parenthesis++; }
											if (closed == -1) throw new Exception("Error.Check () after function. ) is missing");
										}
										else throw new Exception("Error. ')' is missing. Check this and try again");
									}
									index_close_parenthesis = closed;
									if (index_close_parenthesis != 1)
									{
										parameters = Code.Substring(1, index_close_parenthesis - 1);
									}
								}
								else
									index_close_parenthesis = 1;
							else throw new Exception("Error. ) is missing!");


                        tokens.Add(new Token(Code.ElementAt(0).ToString(CultureInfo.InvariantCulture), Types.Parentheses, outputTextBox, display_out));
                        if (parameters != "")
                            ParseTokens(parameters, true,true,false); // розбити на токени те, що в дужках
                        tokens.Add(new Token(Code.ElementAt(index_close_parenthesis).ToString(CultureInfo.InvariantCulture), Types.Parentheses, outputTextBox, display_out));
                        Code = Code.Remove(0, index_close_parenthesis + 1); continue;
                    case '"':
							if (Code.Length > 1)
							{
								int index_close_quote = Code.IndexOf('"', 1);
								if(index_close_quote==-1) throw new Exception("Error. \" is missing!");
								tokens.Add(new Token(Code.ElementAt(0).ToString(CultureInfo.InvariantCulture), Types.Quotes, outputTextBox, display_out));
								tokens.Add(new Token(Code.Substring(1, index_close_quote - 1), Types.Value, outputTextBox, display_out));
								tokens.Add(new Token(Code.ElementAt(index_close_quote).ToString(CultureInfo.InvariantCulture), Types.Quotes, outputTextBox, display_out));
								Code = Code.Remove(0, index_close_quote + 1);
							}
							else throw new Exception("Error. \" is missing!");
							continue;
                    case '[':
							if (Code.Length > 1)
							{
						var index_close_bracket = Code.IndexOf(']', 1);
								if (index_close_bracket == -1) throw new Exception("Error. ] is missing!");
								var param = Code.Substring(1, index_close_bracket - 1);
                        tokens.Add(new Token(Code[0].ToString(CultureInfo.InvariantCulture), Types.Brackets, outputTextBox, display_out));
                        //tokens.Add(new Token(param.ToString(CultureInfo.InvariantCulture),Types.Param, outputTextBox));
                        ParseTokens(param, true, true, false); // розбити на токени те, що в дужках
                        tokens.Add(new Token(Code[index_close_bracket].ToString(CultureInfo.InvariantCulture), Types.Brackets, outputTextBox, display_out));
                        Code = Code.Remove(0, index_close_bracket + 1);
							}
							else throw new Exception("Error. ] is missing!");
							continue;
                    case '/':
                        {
                            if (Code[1].Equals('/')) { Code = Code.Remove(0, Code.IndexOf('\n'));
                                continue;
                            }
                            if (Code[1].Equals('*'))
                            {
                                Code = Code.Remove(0, Code.IndexOf("*/", StringComparison.Ordinal) + 2);
                                continue;
                            }
                        }
                        break;
                    case ';'://віддаємо команду на виконання
                        {
                            tokens.Add(new Token(Code.ElementAt(0).ToString(CultureInfo.InvariantCulture), Types.Semicolon, outputTextBox, display_out));
                            Code = Code.Remove(0, 1);
                            performed = MyTree.CreateTree(tokens, 0, tokens.Count, false);
                            tokens.Clear(); //очищуємо токени
								if(performed) performed = false;
							}
                        continue;
                }
                foreach (var type in MyTree.DataTypes) //тип даних та змінна
                {
                    if ((Code.IndexOf(type, StringComparison.Ordinal) == 0 && Code[type.Length] == ' '))
                    {
                        Code = Code.Remove(0, type.Length + 1);//видаляємо тип
                        List<int> pos_chars = new List<int>
                        {
                            Code.IndexOf(":=", StringComparison.Ordinal),
                            Code.IndexOf(" ", StringComparison.Ordinal),
                            Code.IndexOf(";", StringComparison.Ordinal)
                        };

                        string VarName = Code.Substring(0, pos_chars.Where(p => p > 0).Min());//кінець назви змінної
                        tokens.Add(new Token(VarName, Types.Var, outputTextBox, display_out));//додаємо назву змінної
							try
							{
								Trees.Variables.Add(VarName, new VarInfo(type, null));//додаємо нову змінну до загального списку
							}
                           catch(Exception e)
							{
								throw new Exception("Error. Variable with name "+VarName+" was already added!");
							}
                        Grid.Rows.Add(Trees.Variables.Last().Key, Trees.Variables.Last().Value.Type, "null");
                        Code = Code.Remove(0, VarName.Length);
                        again = true;
                        break;
                    }
                }
                if (again) continue;
                foreach (var operation in Operations)//обробка операцій
                {
                    if (Code.IndexOf(operation, StringComparison.Ordinal) == 0)
                    {
                        tokens.Add(new Token(operation.ToString(CultureInfo.InvariantCulture), Types.Operation, outputTextBox, display_out));
                        Code = Code.Remove(0, operation.Length);
                        again = true;
                        break;
                    }
                }
                if (again) continue;
                foreach (var method in MyTree.Methods.Keys)//Методи
                {
                    if (Code.IndexOf(method, StringComparison.Ordinal) == 0)
                    {
                        tokens.Add(new Token(method.ToString(CultureInfo.InvariantCulture), Types.Method, outputTextBox, display_out));
                        Code = Code.Remove(0, method.Length);
                        next_bracket = true;
                        again = true;
                        break;
                    }
                }
                if (again || next_bracket) continue;//якщо наступними мають бути дужки

                int index;
                foreach (var oper in Operators) //оператори (цикл і умова) ДОРОБИТИ
                {
                    if (Code.IndexOf(oper, StringComparison.Ordinal) == 0)
                    {
                        tokens.Add(new Token(oper.ToString(CultureInfo.InvariantCulture), Types.Operator, outputTextBox, display_out));

                        Code = Code.Remove(0, oper.Length);
                        index = 0;
                        if (tokens[0].value.Equals("}")) index = 1;
                        switch ((string)tokens[index].value)
                        {
                            case "checkIf": //оператор умови
                                {if (Code.Length > 1)
										{
											if (Code.IndexOf('{', 1) != -1)
											{
												var p = Code.Substring(0, Code.IndexOf('{', 1));//вирізати те, що йде після оператора умови або циклу аж до {
												if (p[0].Equals("(") && p[0].Equals(")")) throw new Exception("Error. Empty statement between checkIf(____)");
												if (!p.Equals("")) ParseTokens(p, true, true, false);
												else throw new Exception("Error.Invalid call checkIf operator without (_)");
												ResultPredicate = MyTree.CreateTree(tokens, 0, tokens.Count, true);
												tokens.Clear();
												Code = Code.Remove(0, p.Length);
												p = Code.Substring(0, Code.IndexOf("{", 0));
												if(!p.Equals(""))
												ParseTokens(p, false, true, false);
												if (tokens.Count == 0)
												{
													if(Code.IndexOf("{", 0)>0)
													Code = Code.Remove(0, Code.IndexOf("{", 0));
													Token temp = new Token("{", Tokens.Types.Braces, outputTextBox, display_out);
													int opened_braces = 1, opened = 1, closed = 0;
													while (!Code.ElementAt(closed).Equals("}") && opened_braces != 0)
													{
														if (Code.Length > closed+1)
														{
															if (opened_braces > 0)
															{
																if (Code[closed + 1].Equals("}")) opened_braces--;
																else
																{
																closed = Code.IndexOf("}", closed + 1);
																opened_braces--;
																}	
															}
															else break;
															opened = Code.IndexOf("{", opened + 1);
															if (opened < closed && opened != -1)
															{ opened_braces++; }
															if (closed == -1) throw new Exception("Error.Check { } . } is missing");
														}
														else throw new Exception("Error. '}' is missing!");
													}
													CheckIfClosed = closed;
												}
												else throw new Exception("Error.Invalid using operator checkIf(). You missed {} - body!");
												if (ResultPredicate)
												{
													//без дужок
													string temp = Code.Substring(1, CheckIfClosed - 1);
													if (temp.Length > 0)
														ParseTokens(temp, true, true,true);
													//кидаємо тіло CheckIf на розбиття на токени і виконання
												}
												Code = Code.Remove(0, CheckIfClosed);
												tokens.Clear();
											}
											else throw new Exception("Error. '{' is missing after checkIf");
										}
										else throw new Exception("Error. Nothing after checkIf operator!");
                                }
                                break;
                            case "otherwise":
                                {
									if(tokens.Count>0)
                                    if (tokens[0].value.Equals("}") && tokens.Count == 2)//перевірка чи перед цим був checkIf(){}
                                    {
                                        tokens.Clear();
                                        var p = Code.Substring(0, Code.IndexOf("{", 0));
										if(p.Length>0)
                                        ParseTokens(p, false, true, false);
                                        if (tokens.Count == 0)
                                        {
											if(Code.IndexOf("{", 0)>0)
                                            Code = Code.Remove(0, Code.IndexOf("{", 0));
                                            Token temp = new Token("{", Tokens.Types.Braces, outputTextBox, display_out);
                                            int opened_braces = 1, opened = 1, closed = 0;
												try
												{
													while (!Code.ElementAt(closed).Equals("}") && opened_braces != 0)
													{
														if (opened_braces > 0)
														{
															closed = Code.IndexOf("}", closed + 1);
															opened_braces--;
														}
														else break;
														opened = Code.IndexOf("{", opened + 1);
														if (opened < closed && opened != -1)
														{ opened_braces++; }
														if (closed == -1) throw new Exception("Error. } is missing");
													}
												}
												catch(Exception e)
												{
													throw new Exception("Error. Invalid call otherwise {}");
												}
                                            if (ResultPredicate == false)
                                            {
                                                string temp1 = (Code.Substring(1, closed - 1));
													if (temp1.Length > 0)
														ParseTokens(temp1, true, true,true);//кидаємо тіло otherwise на розбиття на токени і виконання
											}
                                            Token temp11 = new Token("}", Tokens.Types.Braces, outputTextBox, display_out);
                                            Code = Code.Remove(0, closed + 1);
                                        }
                                        else throw new Exception("Error.Invalid using operator otherwise");
                                    }
                                    else throw new Exception("Error.Invalid using otherwise. Allowed only after checkIf(){}!");
									else throw new Exception("Error.Invalid using otherwise{}");
									}
                                break;
                            case "cycle":
                                {
										if (Code.IndexOf('{', 1) > 1)
										{
											var p = Code.Substring(0, Code.IndexOf('{', 1) - 1);//вирізати те, що йде після оператора умови або циклу аж до {
											if (!p.Equals("")) ParseTokens(p, true, true, false);
											else throw new Exception("Error.Invalid call cycle operator without (_)");
											Code = Code.Remove(0, p.Length);
											var temp_tokens = new List<Tokens.Token>();
											if (tokens.Count > 0)
											{
												foreach (var t in tokens)
												{
													temp_tokens.Add(t);
												}
												tokens.Clear();
											}
											else throw new Exception("Error. Invalid call cycle(statement) {}");
											p = Code.Substring(0, Code.IndexOf("{", 0));
											if(p.Length>0)
											ParseTokens(p, false, true, false);
											if (tokens.Count == 0)
											{
												Code = Code.Remove(0, Code.IndexOf("{", 0));
												Token temp = new Token("{", Tokens.Types.Braces, outputTextBox, display_out);
												int opened_braces = 1, opened = 1, closed = 0;
												while (!Code.ElementAt(closed).Equals("}") && opened_braces != 0)
												{
													if (Code.Length>closed)
													{
														if (opened_braces > 0)
														{
															closed = Code.IndexOf("}", closed + 1);
															opened_braces--;
														}
														else break;
														opened = Code.IndexOf("{", opened + 1);
														if (opened < closed && opened != -1)
														{ opened_braces++; }
														if (closed == -1) throw new Exception("Error.Check { } after cycle(). } is missing");
													}
													else throw new Exception("Error. } is missing after cycle(){ ");
												}
												var substring = Code.Substring(1, closed - 1);//вирізаємо тіло циклу
																							  //допоки виконуєтося умова циклу
												bool show = true;
												bool result = MyTree.CreateTree(temp_tokens, 0, temp_tokens.Count, true);//умова циклу
												while (result)
												{
													try
													{
														bool res = ParseTokens(substring, show, true, true);
														if (!res) break;
														if (show) show = false;
														result = MyTree.CreateTree(temp_tokens, 0, temp_tokens.Count, true);//умова циклу
													}
													catch(Exception e)
													{
														throw new Exception("Error. The process is endless");
													}
												}
												Token temp11 = new Token("}", Tokens.Types.Braces, outputTextBox, display_out);//вивід на екран закритої }
												Code = Code.Remove(0, closed);
											}
											else throw new Exception("Error.Invalid call cycle()");
										}
										else throw new Exception("Error. Invalid calling cycle!");
                                }
                                break;
                        }
                        again = true;
                        break;
                    }
                }
                if (again) continue;
                foreach (var varName in Trees.Variables.Keys)//якщо змінна уже занесена у загальну таблицю змінних але відрізняються закінченням
                {
                    if (Code.IndexOf(varName, StringComparison.Ordinal) == 0)
                    {
                        char ending = '\0';
                        if (Code.Length > varName.Length)
                            ending = Code[varName.Length];
                        if (char.IsLetterOrDigit(ending)) continue; //перевірка закінчення
                        tokens.Add(new Token(varName, Types.Var, outputTextBox, display_out));
                        Code = Code.Remove(0, varName.Length);
                        again = true;
                        break;
                    }
                }
                if (again) continue;
                //якщо нічого із вищеперечисленого не підходить - одже це просте значення
                var value = "";
                int[] indexes = new int[]//витягуємо число
                {
                    Code.IndexOf(';'),
                    Code.IndexOf(','),
                    Code.IndexOf('+'),
                    Code.IndexOf('-'),
                    Code.IndexOf('*'),
                    Code.IndexOf('/'),
                    Code.IndexOf('>'),
                    Code.IndexOf('<'),
                    Code.IndexOf('=')
                };
                if (indexes.Any(x => x > 0))
                    index = indexes.Where(x => x > 0).Min();
                else index = Code.Length; //якщо воно було в дужках (як параметр)
                value = Code.Substring(0, index);
                tokens.Add(new Token(value, Types.Value, outputTextBox, display_out));
                Code = Code.Remove(0, value.Length);
            }
			if(tokens.Count>0)
				{
				if (!tokens[0].value.Equals("}"))
			if(Code.Length == 0 && performed==false && tokens.Count>0 && (inRecursion==false || Body == true))
				{
					performed = MyTree.CreateTree(tokens, 0, tokens.Count, false);
					tokens.Clear(); //очищуємо токени
				}
				}
				
        }
            catch (Exception e)
            {
				if (!e.Message.Equals(""))
					if (!e.Message.IndexOf("Error",0).Equals(0))
					{
						string MyError = "Error. Check your code and try again!";
						ErrorHandler(MyError);
						MessageBox.Show(MyError);
					}
					else
					{
						ErrorHandler(e.Message);
						MessageBox.Show(e.Message);
						return false;
					}
				return false;
            }
            return true;
        }
    }
}
