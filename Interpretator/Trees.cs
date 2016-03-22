using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interpretator
{
    internal class Trees
    {
        internal static List<WebBrowser> Browsers = new List<WebBrowser>(); // веб браузери
        internal static TabControl Tabs; // панелі
        internal readonly Dictionary<string, string> Methods;
        internal static Dictionary<string, Tokens.VarInfo> Variables; //змінні і їхні знaчення
		internal readonly List<string> DataTypes; // типи даних
		private readonly RichTextBox outMessageBox;
        public static List<Trees.Command> AllCommands;
        public static List<Page> AllPages;
        public static int IfClosed;
        public static bool ResPredicate;
        public static BrowserF Form2;
        public static RichTextBox output;
        public static string CurrentQuery = "";
        public static RichTextBox Info_RTB;
        public static DataGridView Grid;
        public static RichTextBox ErrorsTextBox;
		public static bool Opened = false;
        public Trees(RichTextBox output1,RichTextBox errors, DataGridView grid, RichTextBox INFO)
        {
            output = output1;
            Grid = grid;
            Info_RTB = INFO;
            ErrorsTextBox = errors;
            Methods = new Dictionary<string, string> {
                    {"Open", "void"},           //Open();
                    {"ShowMes", "void"},        //ShowMes("mes"+"mes");
                    {"Load", "bool"},           //Pages[n].Load("reference");
                    {"GetElementByID", "Element"}, //Pages[n].GetElementByID("id");
                    {"GetElementsByTagName", "Elements"}, //Pages[n].GetElementsByTagName("p");
                    {"NewPages", "bool"},       //NewPages(number);
                    {"DoAction", "bool"},		//Element.DoAction;
					{"Click", "bool"},			//Element.Click;
					{"NewPage", "bool"},		//Click.NewPage;
					{"SetColor", "bool"},		//Element.DoAction.SetColor("color");
					{"GoToPage", "bool"},       //GoToPage(number);
                    {"Count", "integer"},       //Pages.Count();   Element.Count
                    {"ClosePage", "bool"},      //ClosePage(number);
                    {"GetElements", "Elements"}, //Pages[n].GetElements("attribute_title","attribute_value");
                    {"Value", "Element"},		//Element.Value;
                    {"Pages", "Page[]"},
					{"Zoom", "bool"},			//Pages[n].Zoom(int);
					{"Scroll", "bool"},			//Pages[n].Scroll(int,int);
					{"innerText", "line"},		//Element.innerText()
					{"innerHTML", "line"}		//Element.innerHTML()
				};
            Variables = new Dictionary<string, Tokens.VarInfo>();
			DataTypes = new List<string> {"integer", "fractional", "line", "bool","Input",
				"Inputs", "Button", "Buttons","Div", "Divs",
				"Paragraph", "Paragraphs","Element" ,"Elements","Image","Images","Link","Links","Header","Headers","Span","Spans","Qtag","Qtags"
			,"TextArea","TextAreas"};
			outMessageBox = output;
            AllCommands = new List<Command>();
            AllPages = new List<Page>();
            Form2 = new BrowserF();//підключення до другої форми
        }
        void ErrorHandler(string error)
        {
            ErrorsTextBox.Text += "\n   " + error;
        }
        internal class Page // клас web-сторінки
        {
            private string source;

            public Page(string s)
            {
                source = s;
            }
        }
		public static void GetElementByID_Function(ref int i, ref List<Tokens.Token> tokens, Tokens.Token page, out object res)
		{
			res = null;
			if (tokens.Count > i + 6)
			{
				if (tokens[i + 1].value.Equals("(") && tokens[i + 5].value.Equals(")"))
				{
					if (tokens[i + 2].type.Equals(Tokens.Types.Quotes) &&
						tokens[i + 4].type.Equals(Tokens.Types.Quotes)
						&& tokens[i + 6].type.Equals(Tokens.Types.Semicolon))
					{
						AllCommands.Add(new GetElementByID(page, tokens[i + 3], Variables));
						res = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
						AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
						i += 6;
					}
					else throw new Exception("Error.Quotes or semicolon are missing!\nIt must be so GetElementByID(\"id\")");
				}
				else
				{
					throw new Exception("Error in colling function GetElementByID(id)");
				}
			}
			else throw new Exception("Error. Invalid calling of Pages[].GetElementByID function!");
            
		}
		public static void GetElementsByTagName_Function(ref int i, ref List<Tokens.Token> tokens, Tokens.Token page, out object res, bool Var)
		{
			res = null;
			if (tokens.Count > i + 5)
			{
				if (tokens[i + 1].value.Equals("(") && tokens[i + 5].value.Equals(")"))
			{
				if (tokens[i + 2].type.Equals(Tokens.Types.Quotes) &&
					tokens[i + 4].type.Equals(Tokens.Types.Quotes))
				{
					AllCommands.Add(new GetElementsByTagName(page, tokens[i + 3], Variables,Var));
					res = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
					AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
					i += 5;
				}
				else throw new Exception("Error.Quotes or semicolon are missing!\nIt must be so GetElementsByTagName(\"tag_name\")");
			}
			else
			{
				throw new Exception("Error in colling function GetElementsByTagName(tag_name)");
			}
			}
			else throw new Exception("Error. Invalid calling of Pages[].GetElementByTagName function!");
		}
		public static void GetElements_Function(ref int i, ref List<Tokens.Token> tokens, Tokens.Token elem, out object res,bool Var)
		{
			res = null;
			if (tokens.Count > i + 9)
			{
				if (tokens[i + 1].value.Equals("(") && tokens[i + 9].value.Equals(")"))
			{
				var temp = new List<Tokens.Token>();
				if (tokens[i + 2].type.Equals(Tokens.Types.Quotes) &&
					tokens[i + 4].type.Equals(Tokens.Types.Quotes) &&
					tokens[i + 6].type.Equals(Tokens.Types.Quotes) &&
					tokens[i + 8].type.Equals(Tokens.Types.Quotes) &&
					tokens[i + 5].type.Equals(Tokens.Types.Comma))
				{
					temp.Add(tokens[i + 3]);
					temp.Add(tokens[i + 7]);
					AllCommands.Add(new GetElements(elem, temp, Variables,Var)); 
					res = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
					AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
					i += 9;
				}
				else throw new Exception("Error.Invalid structure of parameters in calling function!\nIt must be so GetElements(\"attribute\",\"value\")");
			}
			else
			{
				throw new Exception("Error.Error in colling function GetElements(\"attribute\",\"value\")");
			}
			}
			else throw new Exception("Error. Invalid calling of Pages[].GetElements function!");
		}
		internal static object PagesReturn(ref int i, ref List<Tokens.Token> tokens)
        {
            object res = null;
            int start = i;
            CurrentQuery = "";
            switch ((string)tokens[i + 1].value)//Ok
            {
                case "["://номер вкладки
                    {
                        List<Tokens.Token> temp1 = new List<Tokens.Token>();
						try
						{
							while (!tokens[i + 2].value.Equals("]"))
							{
								temp1.Add(tokens[i + 2]);
								tokens.Remove(tokens[i + 2]);
							}
						}
                        catch(Exception e)
						{
							throw new Exception("Error. Invalid syntax calling Pages[]");
						}
						int result;
                        if (temp1.Count > 0)
						{
							AllCommands.Add(new MyExpression(temp1, "integer", Variables));
							result = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
							AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
						}
						else throw new Exception("Error. There is no parameters between []");

                        tokens.Insert(i + 2, new Tokens.Token(result.ToString(),Tokens.Types.Value,output,false));
						if(tokens.Count>4)
                        if ((tokens[i + 2].type.Equals(Tokens.Types.Value) ||
                             tokens[i + 2].type.Equals(Tokens.Types.Var)) &&
                            tokens[i + 3].value.Equals("]"))
                        {
                            Tokens.Token page_numb = tokens[i + 2];//номер сторінки
							if(tokens.Count>5)
							{
								if (tokens[i + 4].type.Equals(Tokens.Types.Dot))
								{
									i += 5;
									switch ((string)tokens[i].value)//функції
									{
										case "Load"://завантажити URL
											{
													if(tokens.Count>i+6)
													{
														if (tokens[i + 1].value.Equals("(") && tokens[i + 5].value.Equals(")"))
														{
															if (tokens[i + 2].type.Equals(Tokens.Types.Quotes) &&
																tokens[i + 4].type.Equals(Tokens.Types.Quotes)
																&& tokens[i + 6].type.Equals(Tokens.Types.Semicolon))
															{
																AllCommands.Add(new Load(page_numb, tokens[i + 3], Variables));
																res = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
																AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
																i += 6;
															}
															else throw new Exception("Error.Quotes or semicolon are missing!\nIt must be so Load(\"reference\")");
														}
														else
														{
															throw new Exception("Error in colling function Load(\"reference\")");
														}
													}
													else throw new Exception("Error.Invalid calling of Load(\"reference\") function");	
											}
											break;
										case "GetElementByID"://отримати елемент за ID
											{
												GetElementByID_Function(ref i, ref tokens, page_numb, out res);
											}
											break;
										case "GetElementsByTagName"://отримати елемент за назвою
											{
												GetElementsByTagName_Function(ref i, ref tokens, page_numb, out res, false);//повернуло res
											}
											break;
										case "GetElements"://отримати інформацію про елементи із певним атрибутом
											{
												GetElements_Function(ref i, ref tokens, page_numb, out res, false);
											}
											break;
										case "Zoom"://змінити масштаб
												{
													res = null;
													if (tokens.Count > i + 4)
													{
														if (tokens[i + 1].value.Equals("(") && tokens[i + 3].value.Equals(")"))
														{
															List<Tokens.Token> temp = new List<Tokens.Token>();
															temp.Add(tokens[i + 2]);
															tokens.Remove(tokens[i + 2]);
															if (temp.Count > 0)
															{
																AllCommands.Add(new MyExpression(temp, "integer", Variables));
																int Result = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
																AllCommands.Remove(AllCommands[AllCommands.Count - 1]);

																tokens.Insert(i + 2, new Tokens.Token(Result.ToString(), Tokens.Types.Value, output, false));

																if (tokens[i + 3].value.Equals(")") && tokens[i + 4].type.Equals(Tokens.Types.Semicolon))
																{
																	AllCommands.Add(new Zoom(page_numb, tokens[i + 2], Variables));
																	res = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
																	AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
																	i += 4;
																}
																else throw new Exception("Error.Semicolon is missing!\nIt must be so Zoom(number);");
															}
															else
															{
																throw new Exception("Error in colling function Zoom(numb)");
															}
														}
														else throw new Exception("Error. Invalid calling of Pages[].Zoom function!");
													}
												}
												break;
											case "Scroll"://отримати елемент за ID
												{
													res = null;
													int x=0, y=0;
													if (tokens.Count > i + 4)
													{
														if (tokens[i + 1].value.Equals("("))
														{
															List<Tokens.Token> temp = new List<Tokens.Token>();
															try
															{
																while (!(tokens[i + 3].value.Equals(";") && tokens[i + 2].value.Equals(")")))
																{
																	if(tokens[i+2].type.Equals(Tokens.Types.Comma))
																	{
																		if(temp.Count>0)
																		{
																			AllCommands.Add(new MyExpression(temp, "integer", Variables));
																			x = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
																			AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
																		}
																		else throw new Exception("Error in colling function Scroll(numb1,numb2). Nothing before , inside (__ , __)");
																		tokens.Remove(tokens[i + 2]);
																		temp.Clear();
																	}
																	temp.Add(tokens[i + 2]);
																	tokens.Remove(tokens[i + 2]);
																}
															}
															catch (Exception e) { throw new Exception("Error. ; is missing!"); }
															if (temp.Count > 0)
															{
																AllCommands.Add(new MyExpression(temp, "integer", Variables));
																y = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
																AllCommands.Remove(AllCommands[AllCommands.Count - 1]);

																if (tokens[i + 2].value.Equals(")") && tokens[i + 3].type.Equals(Tokens.Types.Semicolon))
																{
																	AllCommands.Add(new Scroll(page_numb,new Tokens.Token(x.ToString(), Tokens.Types.Value, output, false), new Tokens.Token(y.ToString(), Tokens.Types.Value, output, false), Variables));
																	res = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
																	AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
																	i += 4;
																}
																else throw new Exception("Error.Semicolon is missing!\nIt must be so Scroll(number1,number2);");
															}
															else
															{
																throw new Exception("Error in colling function Scroll(numb1,numb2). Nothing after , inside (__ , __)");
															}
														}
														else throw new Exception("Error. Invalid calling of Pages[].Scroll function!");
													}
												}
												break;
											default: throw new Exception("Error. Invalid calling Pages[n]._____. There is no mached function");break;
									}
								}
								else throw new Exception("Error.Invalid using command Pages[]");
							}
							else throw new Exception("Error. Invalid calling Pages[]");
							
                        }
                        else throw new Exception("Error.Invalid using list Pages");
						else throw new Exception("Error.Invalid calling Pages[number].");
					}
                    break;
                case "."://повернення кількості вкладок
                    {
						if(tokens.Count>5)
						{
							if (tokens[i + 2].value.Equals("Count")
							&& tokens[i + 3].value.Equals("(")
							&& tokens[i + 4].value.Equals(")"))
							{
								AllCommands.Add(new Count());
								res = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
								AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
								i += 5;
							}
							else throw new Exception("Error.Invalid using Pages[].Count() function");
						}
						else throw new Exception("Error.Invalid calling function Pages[].Count()");
					}
                    break;
				default: throw new Exception("Error. Invalid symbol after Pages___");break;
            }
            for (int k = start; k < tokens.Count; k++)
            {
                CurrentQuery += tokens[k].value.ToString();
            }
            return res;
        }
		public static void ClickNewPages(ref int i, HtmlElement item)//_____.Click.NewPage
		{
			i += 2;
			AllCommands.Add(new NewPages(new Tokens.Token("1", Tokens.Types.Value, null, false), Variables));
			AllCommands[AllCommands.Count - 1].Execute();
			AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
			string temp = ((!((temp = ((HtmlElement)item).GetAttribute("href")).Equals(""))) ? temp : "");
			AllCommands.Add(new Load(new Tokens.Token((Browsers.Count).ToString(), Tokens.Types.Value, null, false),
			new Tokens.Token(temp, Tokens.Types.Value, null, false), Variables));
			AllCommands[AllCommands.Count - 1].Execute();
			AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
		}
		public static object Element_Function(ref int i, ref List<Tokens.Token> tokens,Tokens.Token CurrentToken)
		{
			object res = null;
			int a = 1;
			switch (tokens[i + 1].value.ToString())
			{
				case ".":
					{ if (tokens.Count > i + 2)
							switch (tokens[i + 2].value.ToString())
							{
								case "Value"://значенн у Input :=
									{
										if (tokens.Count > i + 3)
										{
											if (tokens[i + 3].value.Equals(":="))
											{
												int k = i + 4;
												var temp = new List<Tokens.Token>();
												try
												{
													while (!tokens[k].type.Equals(Tokens.Types.Semicolon))
													{ temp.Add(tokens[k]); k++; }
												}
												catch (Exception e)
												{
													throw new Exception("Error. ; is missing after ___.Value :=____");
												}
												if (temp.Count > 0)
												{
													AllCommands.Add(new MyExpression(temp, "line", Variables));
													string inner = AllCommands[AllCommands.Count - 1].Execute();//виконуємо присвоєння
													AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
													foreach (var VARIABLE in Variables)
													{
														if (VARIABLE.Key.Equals(CurrentToken.value))
														{
															if (VARIABLE.Value.Var != null)
															{
																if (VARIABLE.Value.Type.Equals("Inputs"))
																{
																	if (Assignment.IsList(VARIABLE.Value.Var))
																	{
																		foreach (HtmlElement item in (List<HtmlElement>)VARIABLE.Value.Var)
																		{
																			if (item.TagName.Equals("INPUT"))
																				item.InnerText = inner;
																			else throw new Exception("Error.You can not assign a text value " + ((HtmlElement)VARIABLE.Value.Var).TagName + " element");
																		}
																	}
																	else
																		foreach (HtmlElement item in (HtmlElementCollection)VARIABLE.Value.Var)
																		{
																			if (item.TagName.Equals("INPUT"))
																				item.InnerText = inner;
																			else throw new Exception("Error.You can not assign a text value " + ((HtmlElement)VARIABLE.Value.Var).TagName + " element");
																		}
																}
																else
															if (((HtmlElement)VARIABLE.Value.Var).TagName.Equals("INPUT"))
																{
																	((HtmlElement)VARIABLE.Value.Var).InnerText = inner;
																}
																else throw new Exception("Error.You can not assign a text value " + ((HtmlElement)VARIABLE.Value.Var).TagName + " element");
															}
															else throw new Exception("Error. You can't assign value to input because " + VARIABLE.Key + " is empty! There isn't real element is browser");
														}
													}
													i += k;
													res = true;//присвоєно
												}
												else throw new Exception("Error. There is no parameters after assignment ___.Value :=___");
											}
											else throw new Exception("Error. After ____.Value must be := \"VALUE\"");
										}
										else throw new Exception("Error. Invalid calling ____.Value with nothing");
									}
									break;
								case "DoAction":
									{
										if(tokens.Count>i+4)
										if (tokens[i + 3].type.Equals(Tokens.Types.Dot))
										{
											switch (tokens[i + 4].value.ToString())
											{
												case "Click":
													{
															if(tokens[i + 5].value.Equals("(")&& tokens[i + 6].value.Equals(")"))
															{
																MessageBox.Show("Click?");
																foreach (var VARIABLE in Variables)
																{
																	if (VARIABLE.Key.Equals(CurrentToken.value))
																	{
																		if (VARIABLE.Value.Var != null)
																		{
																			bool NewPagesClick = false;
																			if (tokens.Count > i + 8)
																				if (tokens[i + 7].type.Equals(Tokens.Types.Dot) && tokens[i + 8].value.Equals("NewPage"))//якщо потрібно вікрити при цьому нову сторінку
																				{
																					if (!VARIABLE.Value.Type.Equals("Links") &&
																					   !VARIABLE.Value.Type.Equals("Link")
																					   && !VARIABLE.Value.Type.Equals("Elements"))
																					{
																						throw new Exception("You can use operation Click.NewPage; only to links (elements with tag <a>)");
																					}
																					NewPagesClick = true;
																				}
																				else throw new Exception("Error. Invalid calling Element.Doaction.Click.______. It can be only .NewPage;");
																			if (VARIABLE.Value.Type.Last().Equals('s'))//якщо це множинні елементи
																			{
																				if (Assignment.IsList(VARIABLE.Value.Var))
																				{
																					if (VARIABLE.Value.Var != null)
																						foreach (HtmlElement item in (List<HtmlElement>)VARIABLE.Value.Var)
																						{
																							if (NewPagesClick && item.TagName.Equals("A"))//клікаємо тільки по посиланнях для переходу на нову сторінку
																							{
																								ClickNewPages(ref i, item);
																							}
																							else
																								item.InvokeMember("Click");
																						}
																					else throw new Exception("Error. There aren't existance elements for clicking!");
																				}
																				else if (VARIABLE.Value.Var != null)
																					foreach (HtmlElement item in (HtmlElementCollection)VARIABLE.Value.Var)
																					{
																						if (NewPagesClick && item.TagName.Equals("A"))
																						{
																							ClickNewPages(ref i, item);
																						}
																						else
																							item.InvokeMember("Click");
																					}
																				else throw new Exception("Error. There aren't existance elements for clicking!");
																			}
																			else
																			{
																				if (VARIABLE.Value.Var != null)
																				{
																					if (Assignment.IsList(VARIABLE.Value.Var))
																					{
																						foreach (HtmlElement item in (List<HtmlElement>)VARIABLE.Value.Var)
																						{
																							if (NewPagesClick && item.TagName.Equals("A"))
																							{
																								ClickNewPages(ref i, item);
																							}
																							else
																								item.InvokeMember("Click");
																						}
																					}
																					else
																					if (NewPagesClick && ((HtmlElement)VARIABLE.Value.Var).TagName.Equals("A"))//відкриважмо  нову сторінку тільки за посиланням
																					{
																						ClickNewPages(ref i, ((HtmlElement)VARIABLE.Value.Var));
																					}
																					else ((HtmlElement)VARIABLE.Value.Var).InvokeMember("Click");
																				}
																			}
																		}
																		else throw new Exception("Error. There is no element for clicking! " + VARIABLE.Key + " = null!");
																	}
																}
																i += 7;
																if (!tokens[i].type.Equals(Tokens.Types.Semicolon))
																{
																	throw new Exception("Error. ; is missing after Click");
																}
																res = true;
															}
															else throw new Exception("Error. () are missing after Click");
													}
													break;
												case "SetColor"://встановлює колір для компоненти або кількох компонент (HTMLElement)
													{
														MessageBox.Show("SetColor");
														i += 4;
															if(tokens.Count>i+6)
														if (tokens[i + 1].value.Equals("(") && tokens[i + 5].value.Equals(")"))
														{
															if (tokens[i + 2].type.Equals(Tokens.Types.Quotes) &&
																tokens[i + 4].type.Equals(Tokens.Types.Quotes))
															{
																foreach (var VARIABLE in Variables)
																{
																	if (VARIABLE.Key.Equals(CurrentToken.value))
																	{
																				if(VARIABLE.Value.Var!=null)
																				{
																				Assignment.SetColor(VARIABLE.Value.Var, tokens[i + 3].value.ToString());
																				break;
																				}
																		else throw new Exception("Error. There is no element for painting! " + VARIABLE.Key + " = null!");
																			}
																}
																i += 6;
																if (!tokens[i].type.Equals(Tokens.Types.Semicolon))
																{
																	throw new Exception("Error. ; is missing after SetColot(\"color\")");
																}
																res = true;
															}
															else throw new Exception("Error.Quotes or semicolon are missing!\nIt must be so SetColor(\"color\")");
														}
														else
														{
															throw new Exception("Error in colling function SetColor(\"color\")");
														}
															else
															{
																throw new Exception("Error in colling function SetColor");
															}
														}
													break;
												default: throw new Exception("Error.Missing command after Elem.DoAction.____"); break;
											}
										}
										else throw new Exception("Error.Missing .Command after Elem.DoAction");
										else throw new Exception("Error.There is no parameters after Element.DoAction.______");
									}
									break;
								case "Count"://повертає кільксть HTMLElement у змінній
									{
										if(tokens.Count>i+5)
										{
											if (tokens[i + 3].value.Equals("(") && tokens[i + 4].value.Equals(")"))
											{
												foreach (var VARIABLE in Variables)
												{
													if (VARIABLE.Key.Equals(CurrentToken.value))
													{
														if (Assignment.IsList(VARIABLE.Value.Var))
														{
															res = ((List<HtmlElement>)VARIABLE.Value.Var).Count;
														}
														else
														if (Assignment.IsCollection(VARIABLE.Value.Var))
														{
															res = ((HtmlElementCollection)VARIABLE.Value.Var).Count;
														}
														else res = 1;
													}
												}
												i += 5;
												if(!tokens[i].type.Equals(Tokens.Types.Semicolon))
													throw new Exception("Error. Invalid call function Element.___.Count() without ;");
											}
											else
												throw new Exception("Error. Invalid call function Element.___.Count()");
                                        }
										else
											throw new Exception("Error. Invalid call function Element.___.Count();");

									}
									break;
								case "GetElementByID":
									{
										throw new Exception("Error. This function isn't available. Use GetElements(\"id\",\"ID\") instead.");
									}
									break;
								case "GetElementsByTagName":
									{
										i += 2;
										var tok = new Tokens.Token(CurrentToken.value + ".GetElementsByTagName", Tokens.Types.Var, null, false);
										GetElementsByTagName_Function(ref i, ref tokens, CurrentToken, out res, true);//res пoвернуте (об'єкти)
										var info = new Tokens.VarInfo("Elements", null);
										info.Var = res;
										Variables.Add(tok.value, info);
										Assignment.SetColor(res);
										var temp = Element_Function(ref i, ref tokens, tok);
										if (temp != null) res = temp;
									}
									break;
								case "GetElements":
									{
										i += 2;
										var tok = new Tokens.Token(CurrentToken.value + ".GetElementsByTagName", Tokens.Types.Var, null, false);
										GetElements_Function(ref i, ref tokens, CurrentToken, out res, true);//res пoвернуте (об'єкти)
										var info = new Tokens.VarInfo("Elements", null);
										info.Var = res;
										Variables.Add(tok.value, info);
										Assignment.SetColor(res);
										var temp = Element_Function(ref i, ref tokens, tok);
										if (temp != null) res = temp;
									}
									break;
								case "innerText":
									{
										i += 2;
										if(tokens.Count>i+3)
										if (tokens[i + 1].value.Equals("(") && 
											tokens[i + 2].value.Equals(")") && 
											tokens[i + 3].type.Equals(Tokens.Types.Semicolon))
										{
											AllCommands.Add(new innerText(CurrentToken, Variables));
											res = AllCommands[AllCommands.Count - 1].Execute();//виконуємо присвоєння
											AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
												i += 3;
										}
										else throw new Exception("Error. Invalid call function innerText without ();");
										else throw new Exception("Error. Invalid call function innerText without ();");
									}
									break;
								case "innerHTML":
									{
										i += 2;
										if (tokens.Count > i + 3)
											if (tokens[i + 1].value.Equals("(") &&
												tokens[i + 2].value.Equals(")") &&
												tokens[i + 3].type.Equals(Tokens.Types.Semicolon))
											{
											AllCommands.Add(new innerHTML(CurrentToken, Variables));
											res = AllCommands[AllCommands.Count - 1].Execute();//виконуємо присвоєння
											AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
												i += 3;
											}
											else throw new Exception("Error. Invalid call function innerHTML without ();");
										else throw new Exception("Error. Invalid call function innerHTML without ();");
									}
									break;
								default: throw new Exception("Error.Missing command after Elem.____"); break;
							}
						else throw new Exception("Error. No parameters after Element._____");
					}
					break;
				case ";": { i++; res = null;}break;
				
				default:
					{
						throw new Exception("Error.Invalid declaring of variable " + tokens[i].value.ToString());
					}
					break;
			}
			return res;
		}
		public static object Var_index(ref int i, ref List<Tokens.Token> tokens, Tokens.Token CurrentToken) //Виконання операції Element[numb] і повернення елементу колекції
		{
			object res = null;
			List<Tokens.Token> temp1 = new List<Tokens.Token>();
			if(tokens.Count>i+2)
			{
				while (!tokens[i + 2].value.Equals("]"))
				{
					temp1.Add(tokens[i + 2]);
					tokens.Remove(tokens[i + 2]);
				}
				AllCommands.Add(new MyExpression(temp1, "integer", Variables));//визначення числа між []
				int result = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
				AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
				i += 2;
				foreach (var VARIABLE in Variables)
				{
					if (VARIABLE.Key.Equals(CurrentToken.value))//знаходження змінної
					{
						if (VARIABLE.Value.Type.Last().Equals('s'))//множинна змінна
						{
							if (Assignment.IsList(VARIABLE.Value.Var))
							{
								object List_element;
								if (VARIABLE.Value.Var != null)
								{
									if (result <= ((List<HtmlElement>)VARIABLE.Value.Var).Count && result > 0)
									{
										res = List_element = ((List<HtmlElement>)VARIABLE.Value.Var)[result-1];//один елемент із списку
										Assignment.SetColor(((HtmlElement)List_element));
										var T = new Tokens.VarInfo("Elements", null);
										T.Var = List_element;
										Variables.Add((CurrentToken.value + "[" + result + "]").ToString(), T);//додавання нової змінної!!!
										//подивимся що далі є)
										object temp = Element_Function(ref i, ref tokens, new Tokens.Token(Variables.Last().Key, Tokens.Types.Var, null, false));
										if (temp != null) res = temp;
										break;
									}
									else throw new Exception("Index is out of range");
								}
								else throw new Exception("Error. There aren't existance elements for doing operation");
							}
							else if (VARIABLE.Value.Var != null)
							{
								object Collection_element;
								if (result < ((HtmlElementCollection)VARIABLE.Value.Var).Count && result>0)//для колекції
								{
									res = Collection_element = ((HtmlElementCollection)VARIABLE.Value.Var)[result];
									Assignment.SetColor(((HtmlElement)Collection_element));
									var T = new Tokens.VarInfo("HtmlElement", null);
									T.Var = Collection_element;
									Variables.Add(CurrentToken.value + "[" + result + "]", T);
									object temp = Element_Function(ref i, ref tokens, new Tokens.Token(Variables.Last().Key, Tokens.Types.Var, null, false));
									if (temp != null) res = temp;
									break;
								}
								else throw new Exception("Index is out of range");
							}
							else throw new Exception("Error. There aren't existance elements for doing operation");
						}
						else// [] тільки для множинних змінних
						{
							throw new Exception("Error. You can't use operation [] to single element!");
						}
					}
				}
			}
			else throw new Exception("Error. Invalid using [] in Variable[] expression");
			return res;
		}
        internal bool CreateTree(List<Tokens.Token> tokens, int start_tokens, int end_tokens, bool Condition)
        {
            int i = start_tokens;
            try
            {
                if (Condition)//якщо передається умова checkIf oe cycle (____)
                {
                    bool result;
                    if (tokens[0].type.Equals(Tokens.Types.Operator)) tokens.RemoveAt(0);
                    AllCommands.Add(new MyExpression(tokens, "bool", Variables));
                    result = AllCommands[AllCommands.Count - 1].Execute();//виконуємо присвоєння
                    AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
                    return result;
                }
				if (tokens.Count == 1) throw new Exception("Error. Invalid command '"+ tokens[0].value+"' . Check this and try again.");
                while (i < tokens.Count()-1)
                {
                    Tokens.Token CurrentToken = tokens[i];
                    switch (CurrentToken.type)
                    {
                        case Tokens.Types.Var://OK
                            {
								switch (tokens[i + 1].value)
								{
									case ":=":
										{//присвоєння
											var temp = new List<Tokens.Token>();
											temp.Add(tokens[i]);
											//переписування виразу і виконання присвоєння
											while (!tokens[i].type.Equals(Tokens.Types.Semicolon))
											{
												if (tokens.Count > i + 1)
													i++;
												else throw new Exception("Error. ';' is missing after assignment "+CurrentToken.value +" := ...");
												temp.Add(tokens[i]);
											}
											i++;
											if (temp.Count > 3)
											{
												AllCommands.Add(new Assignment(temp, Variables, tokens));
												AllCommands[AllCommands.Count - 1].Execute();//виконуємо присвоєння
												AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
											}
											else throw new Exception("Error. There is no data after "+CurrentToken.value+" := ____;");
										}
										break;
									case ".":
										{
											Element_Function(ref i, ref tokens,CurrentToken);// обробка функцій елементу
										}
										break;
									case ";":
										{
                                         i += 2; break;
										}
										break;
									case "[":
										{
											Var_index(ref i, ref tokens, CurrentToken);
                                        }
										break;
									default: { throw new Exception("Error. Invalid declaration of variable "+CurrentToken.value); }break;
								}
                            }
                            break;
                        case Tokens.Types.Method: //методи
                            {
                                switch ((string)CurrentToken.value)
                                {
                                    case "Open": //Відкриття браузера
                                        {
											if(tokens.Count>3)
                                            if ((tokens[i + 1].value.Equals("(")) && (tokens[i + 2].value.Equals(")")) && (tokens[i + 3].value.Equals(";")))
                                            {
                                                AllCommands.Add(new Open());
                                                AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
                                                AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
                                                i += 3;
                                            }
                                            else throw new Exception("Error.Wrong call of function Open()");
											else throw new Exception("Error.Wrong call of function Open(). There isn't enough element for calling function properly!");
										}
                                        break;
                                    case "ShowMes": //Вивід повідомлення 
                                        {
											
											if (tokens.Count > 4)
												if (tokens[i + 1].value.Equals("("))
                                            {
                                                i += 2;
                                                var temp = new List<Tokens.Token>();
													try
													{
														while (i < tokens.Count)
														{
															if (tokens[i].value.Equals(")"))
																break;
															temp.Add(tokens[i]);
															i++;
														}
													}
                                              catch(Exception e)
													{
														throw new Exception("Error. Inside calling of SowMes() function");
													}
												if(tokens.Count>i)
													{
													if (tokens[i + 1].value.Equals(";"))
													 {//виконання команди
                                                    AllCommands.Add(new ShowMes(temp, Variables));
                                                    AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
                                                    AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
                                                    i++;
													 }
														else throw new Exception("Error.; is missing after ShowMes()");
													}
                                                else throw new Exception("Error.Invalid call of function ShowMes()");
                                            }
                                            else throw new Exception("Error.Wrong call of function ShowMes()");
											else throw new Exception("Error.Wrong call of function ShowMes(). There isn't enough element for calling function properly!");

										}
                                        break;
                                    case "NewPages"://Створення нових вкладок
                                        {
                                            if (tokens[i + 1].value.Equals("("))
                                            {
                                                List<Tokens.Token> temp1 = new List<Tokens.Token>();
												try
												{
												while (!(tokens[i + 3].value.Equals(";") && tokens[i + 2].value.Equals(")")))
                                                {
                                                    temp1.Add(tokens[i + 2]);
                                                    tokens.Remove(tokens[i + 2]);
													 
                                                }
												}
												catch (Exception e) { throw new Exception("Error. ; is missing!"); }
                                                if(temp1.Count>0)
												{
												AllCommands.Add(new MyExpression(temp1, "integer", Variables));
                                                int result = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
                                                AllCommands.Remove(AllCommands[AllCommands.Count - 1]);

                                                tokens.Insert(i + 2, new Tokens.Token(result.ToString(), Tokens.Types.Value, output, false));

                                                if ((tokens[i + 3].value.Equals(")")) && (tokens[i + 4].value.Equals(";")))
                                                {
                                                    AllCommands.Add(new NewPages(tokens[i + 2], Variables));
                                                    AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
                                                    AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
                                                    i += 4;
                                                }
                                                else throw new Exception("Error.Too much parameters for function NewPages(one);");
												}
                                               else throw new Exception("Error. There is no parameters for calling function NewPages(____);");
											}
                                            else throw new Exception("Error.Wrong call of function NewPages()");

                                        }
                                        break;

                                    case "GoToPage"://Перехід між вкладками
                                        {
                                            if (tokens[i + 1].value.Equals("("))
                                            {
                                                List<Tokens.Token> temp1 = new List<Tokens.Token>();
												try
												{
													while (!(tokens[i + 3].value.Equals(";") && tokens[i + 2].value.Equals(")")))
													{
														temp1.Add(tokens[i + 2]);
														tokens.Remove(tokens[i + 2]);
													}
												}
                                                catch(Exception e)
												{
													throw new Exception("Error. ; is missing!");
												}
												if(temp1.Count>0)
												{
													AllCommands.Add(new MyExpression(temp1, "integer", Variables));
													int result = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
													AllCommands.Remove(AllCommands[AllCommands.Count - 1]);

													tokens.Insert(i + 2, new Tokens.Token(result.ToString(), Tokens.Types.Value, output, false));
													if ((tokens[i + 3].value.Equals(")")) && (tokens[i + 4].value.Equals(";")))
													{
														AllCommands.Add(new GoToPage(tokens[i + 2], Variables));
														AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
														AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
														i += 4;
													}
													else throw new Exception("Error.Too much parameters for function GoToPage(one);");
												}
												else throw new Exception("Error.There is no parameters for calling function GoToPage(___);");
											}
                                            else throw new Exception("Error.Wrong call of function GoToPage()");

                                        }
                                        break;
                                    case "ClosePage"://Закриття вкладки
                                        {
                                            if (tokens[i + 1].value.Equals("("))
                                            {
                                                List<Tokens.Token> temp1 = new List<Tokens.Token>();
												try
												{
												while (!(tokens[i + 3].value.Equals(";") && tokens[i + 2].value.Equals(")")))
                                                {
                                                    temp1.Add(tokens[i + 2]);
                                                    tokens.Remove(tokens[i + 2]);
                                                }
												}
												catch (Exception e)
												{
													throw new Exception("Error. ; is missing!");
												}
												if (temp1.Count > 0)
												{
													AllCommands.Add(new MyExpression(temp1, "integer", Variables));
													 int result = AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
													AllCommands.Remove(AllCommands[AllCommands.Count - 1]);

													tokens.Insert(i + 2, new Tokens.Token(result.ToString(), Tokens.Types.Value, output, false));
													if (tokens[i + 3].value.Equals(")") &&
												     tokens[i + 4].value.Equals(";"))
												  {
													   AllCommands.Add(new ClosePage(tokens[i + 2], Variables));
													    AllCommands[AllCommands.Count - 1].Execute();//виконуємо 
													   AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
													    i += 4;
													}
													else throw new Exception("Error.Too much parameters for function ClosePage(one);");
												}
												else throw new Exception("Error.There is no parameters for calling function ClosePage(___);");
											}
                                            else throw new Exception("Error.Wrong call of function ClosePage()");

                                        }
                                        break;
                                    case "Pages": //Вкладки
                                        {
                                            PagesReturn(ref i, ref tokens);
                                        }
                                        break;
                                }
                            }
                            continue; //кінець методів
                        case Tokens.Types.Operation:// операції
                            {
                                switch ((string)CurrentToken.value)
                                {
                                    case "+++"://Інкрементація
                                        {
											if(tokens.Count>i+4)
											{
												if (tokens[i + 1].value.Equals("("))
												{
													if (tokens[i + 2].type.Equals(Tokens.Types.Var) &&
														tokens[i + 3].value.Equals(")"))
													{
														var Var = Variables.Where(x => x.Key.Equals(tokens[i + 2].value)).First();
														if(Var.Value.Value!=null)
														{
															if (Var.Value.Type.Equals("integer") || Var.Value.Type.Equals("fractional"))
															{
																if (Var.Value.Type.Equals("fractional"))
																	MessageBox.Show("Warning. Fractional value will be trancated");
																Var.Value.Value = (Converter(Var.Value.Value, "integer") + 1).ToString();
																for (int i1 = 0; i1 < Grid.RowCount; i1++)
																{
																	if (Grid.Rows[i1].Cells[0].Value.Equals(Var.Key))
																	{
																		Grid.Rows[i1].Cells[2].Value = Var.Value.Value;
																		break;
																	}
																}
																i += 4;
																if (!tokens[i].type.Equals(Tokens.Types.Semicolon))
																	throw new Exception("Error. ; is missing after +++("+Var.Key+")");
															}
															else MessageBox.Show("Error.You can increment only integer variable!");
														}
														else throw new Exception("Error. This variable can't be icremented. It hasn't value");
													}
													else
														throw new Exception("Error.Invalid parameter between parentheses (integer variable)\nCheck you data between (___) and try agan");
												}
												else
													throw new Exception("Error.Wrong call of function increment");
											}
											else
												throw new Exception("Error.Wrong call of function increment. It must be so +++(variable);");
										}
                                        break;
                                    case "---"://Декрементація
                                        {
											if(tokens.Count>i+4)
											{
												if (tokens[i + 1].value.Equals("("))
												{
													if (tokens[i + 2].type.Equals(Tokens.Types.Var) &&
														tokens[i + 3].value.Equals(")"))
													{
														var Var = Variables.Where(x => x.Key.Equals(tokens[i + 2].value)).First();
														if(Var.Value.Value != null)
														{
															if (Var.Value.Type.Equals("integer") || Var.Value.Type.Equals("fractional"))
																	{
																		if (Var.Value.Type.Equals("fractional"))
																			MessageBox.Show("Warning. Fractional value will be trancated");
																		Var.Value.Value = (Converter(Var.Value.Value, "integer") - 1).ToString();
																		for (int i1 = 0; i1 < Grid.RowCount; i1++)
																		{
																			if (Grid.Rows[i1].Cells[0].Value.Equals(Var.Key))
																			{
																				Grid.Rows[i1].Cells[2].Value = Var.Value.Value;
																				break;
																			}
																		}
																		i += 4;
																if (!tokens[i].type.Equals(Tokens.Types.Semicolon))
																	throw new Exception("Error. ; is missing after +++(" + Var.Key + ")");
															}
														else throw new Exception("Error.You can decrement only integer variable!");
														}
														else throw new Exception("Error. This variable can't be decremented. It hasn't value");

													}
													else
														throw new Exception("Error.Invalid parameter between parentheses");
												}
												else
													throw new Exception("Error.Wrong call of function decrement");
											}
											else
												throw new Exception("Error.Wrong call of function decrement. It must be so ---(variable);");
										}
                                        break;
                                    default:throw new Exception("Invalid operation in this context!!! "+CurrentToken.value);break;
                                }

                            }
                            continue;//кінець операцій
                                     //пропускаємо дужки та ;
                        case Tokens.Types.Braces:
                            {
                                i++;
                                continue;
                            }
                        case Tokens.Types.Semicolon:
                            {
                                i++;
                                continue;
                            }
                        default: throw new Exception("Invalid data."+CurrentToken.value+ " doesn't apply to any type ");break;       
                    }
                }
            }
            catch (Exception e)
            {
				throw new Exception(e.Message);
            }
            return true;
        }
        //-------------------------опис методів --------------------------
        public static readonly Dictionary<string, int> Priority = new Dictionary<string, int>//пріорітети операцій
        {
             {"(",-1}, {")",-1},{"+",0}, {"-",0}, {"*",1}, {"/",1}, {":=",-2}, {"+++",3}, {"---",3}
        };


        //ShowMes
        #region

        private class ShowMes : ExecuteCommand //Вивід повідомлення
        {
            private string message = "";

            public ShowMes(List<Tokens.Token> param, Dictionary<string, Tokens.VarInfo> variables)
            {
                foreach (var p in param)
                {
                    if (p.type.Equals(Tokens.Types.Comma))
                        throw new Exception(
                            "Error.Invalid parameters in function ShowMes(). Use only one string parameter!");
                }
                for (int i = 0; i < param.Count; i++)
                {
                    if (param[i].type.Equals(Tokens.Types.Quotes) && param[i + 2].type.Equals(Tokens.Types.Quotes))
                    {
                        message += param[i + 1].value.ToString();
                        i += 3;
                    }
                    else if (param[i].value.Equals("+"))
                    {
                        i++;
                    }
                    else if (param[i].type.Equals(Tokens.Types.Var))
                    {
                        var Var = variables.Where(x => x.Key.Equals(param[i].value)).First();
                        message += Var.Value.Value;
                        i++;
                    }
                    else throw new Exception("Error.Invalid parameters in function ShowMes()");
                }
            }

            public override dynamic Execute()
            {
                MessageBox.Show(message.ToString());
                return true;
            }
        }

        #endregion

        //Load

        #region

        private class Load : ExecuteCommand
        {
            private string reference;
            private int number;

            public Load(Tokens.Token page_number, Tokens.Token reference, Dictionary<string, Tokens.VarInfo> variables)
            {
                this.reference = reference.value.ToString();
                if (page_number.type.Equals(Tokens.Types.Var))
                {
                    var res = variables.Where(i => i.Key.Equals(page_number.value)).First();
                    number = Converter(res.Value.Value,"integer");
                }
                else
                {
                    number = Converter(page_number.value, "integer");
                }
                MessageBox.Show("Load " + reference.value);
            }

            public override dynamic Execute()
            {
					if (Opened)
					{
					if (Browsers.Count < number) throw new Exception("Error. There is no Tab (Page) with that number. Check the number and try again");
						Browsers[number - 1].Navigate(reference);
						while (Browsers[number - 1].ReadyState != WebBrowserReadyState.Complete)
							Application.DoEvents();
						Browsers[number - 1].Document.Body.Style = "zoom:70%";
						return true;
					}
					else
					{
						throw new Exception("Error. Browser is not opened. Use Open() first");
						return false;
					}
            }
        }

		#endregion
		//Zoom

		#region

		private class Zoom : ExecuteCommand
		{
			private int Percent;
			private int page_number;
			public Zoom(Tokens.Token numb, Tokens.Token percent, Dictionary<string, Tokens.VarInfo> variables)
			{
				if(percent.type.Equals(Tokens.Types.Value))//перетворення проценту
				{
					if (!Int32.TryParse(((string)percent.value), out Percent))
						throw new Exception("Error. Invalid parameter of Zoom function. It should be integer");
				}
				else if (percent.type.Equals(Tokens.Types.Var))
				{
					var VAR = variables.Where(i => i.Key.Equals(percent.value)).First();
					Percent = Converter(VAR.Value.Value, "integer");
				}
				else throw new Exception("Error. Invalid parameter of Zoom function. It must be integer");
				if (numb.type.Equals(Tokens.Types.Var))
				{
					var res = variables.Where(i => i.Key.Equals(numb.value)).First();
					page_number = Converter(res.Value.Value, "integer");
				}
				else
				{
					page_number = Converter(numb.value, "integer");
				}
				page_number--;
			}

			public override dynamic Execute()
			{
				if (Opened)
				{
					if ((page_number < 0 || page_number > Browsers.Count))
					{
						throw new Exception("Error. PageNumber is out of range!");
						return false;
					}
					var htmlDocument = Browsers[page_number].Document;
					if (htmlDocument != null)
					{
						htmlDocument.Body.Style = "zoom:"+Percent.ToString()+"%";
                    }
					else
					{
						MessageBox.Show("Error.There is no Page with number " + page_number);
					}
				}
				else
				{
					throw new Exception("Error. Browser is not opened. Use Open() first");
				}
				return true;

			}
		}

		#endregion
		//Scroll

		#region

		private class Scroll : ExecuteCommand
		{
			private int x=0,y=0;
			private int page_number;
            public Scroll(Tokens.Token numb,Tokens.Token X, Tokens.Token Y, Dictionary<string, Tokens.VarInfo> variables)
			{
				if(!Int32.TryParse((X.value.ToString()),out x)) throw new Exception("Error. X is invalid in the Scroll function");
				if (!Int32.TryParse((Y.value.ToString()), out y)) throw new Exception("Error. Y is invalid in the Scroll function");
				if (numb.type.Equals(Tokens.Types.Var))
				{
					var res = variables.Where(i => i.Key.Equals(numb.value)).First();
					page_number = Converter(res.Value.Value, "integer");
				}
				else
				{
					page_number = Converter(numb.value, "integer");
				}
				page_number--;
			}

			public override dynamic Execute()
			{
				if (Opened)
				{
					if ((page_number < 0 || page_number > Browsers.Count))
					{
						throw new Exception("Error. PageNumber is out of range!");
						return false;
					}
					var htmlDocument = Browsers[page_number].Document;
					if (htmlDocument != null)
					{
						htmlDocument.Window.ScrollTo(x,y);
					}
					else
					{
						MessageBox.Show("Error.There is no Page with number " + page_number);
					}
				}
				else
				{
					throw new Exception("Error. Browser is not opened. Use Open() first");
				}
				return true;

			}
		}

		#endregion
		//GetElementByID

		#region

		private class GetElementByID : ExecuteCommand
        {
            private string Id;
            private int page_number;
			private bool Var;
			HtmlElement Element = null;
			public GetElementByID(Tokens.Token numb, Tokens.Token id, Dictionary<string, Tokens.VarInfo> variables)
            {
				 this.Id = id.value.ToString();
				if (numb.type.Equals(Tokens.Types.Var))
                {
                    var res = variables.Where(i => i.Key.Equals(numb.value)).First();
                    page_number = Converter(res.Value.Value, "integer");
                }
                else
                {
                    page_number = Converter(numb.value, "integer");
                }
                page_number--;
				MessageBox.Show("GetElementById " + Id);//page_number -=1
            }

            public override dynamic Execute()
            {
				if(Opened)
				{
					if ((page_number < 0 || page_number > Browsers.Count))
					{
						throw new Exception("Error. PageNumber is out of range!");
						return null;
					}
					var htmlDocument = Browsers[page_number].Document;
					if (htmlDocument != null)
					{
						var element = htmlDocument.GetElementById(Id);
						if (element != null)
						{
							return element;
						}
						MessageBox.Show("Error.Function GetElementById didn't find needed element. Try again!");
					}
					else
					{
						MessageBox.Show("Error.There is no Page with number " + page_number);
					}
				}
				else
				{
					throw new Exception("Error. Browser is not opened. Use Open() first");
				}
				return null;
				
            }
        }

        #endregion

        //GetElementsByTagName

        #region

        private class GetElementsByTagName : ExecuteCommand
        {
            private string TagName;
            private int page_number;
			private bool Var;
			HtmlElement Element = null;
			public GetElementsByTagName(Tokens.Token numb, Tokens.Token name, Dictionary<string, Tokens.VarInfo> variables, bool var)
            {
				Var = var;
				this.TagName = name.value.ToString();
				if (!Var)
				{
				if (numb.type.Equals(Tokens.Types.Var))
                {
                    var res = variables.Where(i => i.Key.Equals(numb.value)).First();
                    page_number = Converter(res.Value.Value,"integer"); 
                }
                else
                {
                    page_number = Converter(numb.value,"integer");
                }
                page_number--;
				}
				else
				{
					foreach (var VARIABLE in variables)
					{
						if (VARIABLE.Key.Equals(numb.value))//numb - елемент
						{
							Element = ((HtmlElement)VARIABLE.Value.Var);
							break;
						}
					}
				}

				MessageBox.Show("GetElementsByTagName " + TagName);
            }

            public override dynamic Execute()//page_number -=1
            {
				if(Opened)
				{
					if (TagName.Equals("")) throw new Exception("Error. GetElementsByTagName tagName is empty!");
					if ((page_number < 0 || page_number > Browsers.Count) && Var == false)
					{
						throw new Exception("Error. PageNumber is out of range!");
						return null;
					}
					HtmlDocument htmlDocument = null;
					if (Var)
					{
						if (Element == null)
						{
							throw new Exception("Error. Element is null!!!");
							return null;
						}
					}
					else htmlDocument = Browsers[page_number].Document;
					if (htmlDocument != null || Element != null)
					{
						var selected = new List<HtmlElement>();
						HtmlElementCollection elements = null;
						if (Var) elements = Element.GetElementsByTagName(TagName);//поверне усі елементи
						else elements = htmlDocument.GetElementsByTagName(TagName);
						return elements;
					}
					else
					{
						MessageBox.Show("There is no Page with number " + page_number);
					}
				}
				else
				{
					throw new Exception("Error. Browser is not opened. Use Open() first");
				}
				return null;
			}
        }

        #endregion

        //NewPages 

        #region

        private class NewPages : ExecuteCommand
        {
            private int number;
            public NewPages(Tokens.Token numb, Dictionary<string, Tokens.VarInfo> variables)
            {
                if (numb.type.Equals(Tokens.Types.Var))
                {
                    var res = variables.Where(i => i.Key.Equals(numb.value)).First();
                    number = Converter(res.Value.Value,"integer");
                }
                else
                {
                    number = Converter(numb.value,"integer");
                }
                MessageBox.Show("NewPages");
            }

            public override dynamic Execute()
            {
                int i = 2;
				if (Opened)
				{
					while (number-- > 0)
					{
						Form2.tabControl.TabPages.Add("Tab" + i++);
						Browsers.Add(new WebBrowser());
						int last = Form2.tabControl.TabCount - 1;
						Browsers[last].Dock = DockStyle.Fill;
						Form2.tabControl.TabPages[last].Controls.Add(Browsers[last]);
						Browsers[last].Navigate("http://www.google.com");
						while (Browsers[last].ReadyState != WebBrowserReadyState.Complete)
							Application.DoEvents();
						Browsers[last].Document.Body.Style = "zoom:70%";
					}
					return true;
				}
				else
				{
					throw new Exception("Error. Browser isn't opened. Use Open() first");
					return false;
				}

            }
        }

        #endregion

        //GoToPage

        #region

        private class GoToPage : ExecuteCommand
        {
            private int page_number;

			public GoToPage(Tokens.Token param, Dictionary<string, Tokens.VarInfo> variables)
			{

				if (param.type.Equals(Tokens.Types.Var))
				{
					var res = variables.Where(i => i.Key.Equals(param.value)).First();
					page_number = Converter(res.Value.Value, "integer");
				}
				else
				{
					page_number = Converter(param.value, "integer");
				}

				MessageBox.Show("GoToPage " + page_number);
				page_number--;
            }
            public override dynamic Execute()
            {
			if(Opened)
			{
				if (page_number < Form2.tabControl.TabCount)
                    Form2.tabControl.SelectTab(Form2.tabControl.TabPages[page_number]);
                else throw new Exception("Error. You are out of range of Tabs");
                return true;
			}
			else
			{
				throw new Exception("Error. Browser isn't opened. Use Open() first");
				return false;
			}
		}
        }

		#endregion
		// innerText

		#region

		private class innerText : ExecuteCommand // відкриває браузер
		{
			HtmlElement elem;
			public innerText(Tokens.Token Element, Dictionary<string, Tokens.VarInfo> variables)
			{
				foreach(var VARIABLE in variables)
				{
					if(VARIABLE.Key.Equals(Element.value))
					{
						if (!Assignment.IsList(VARIABLE.Value.Var) && !Assignment.IsCollection(VARIABLE.Value.Var))
						{
							elem = ((HtmlElement)VARIABLE.Value.Var);
						}
						else throw new Exception("Error. Invalid using function innerText().\n It must be so SinglElement.innerText(). Your element is multiple");
					}
				}
			}
			public override dynamic Execute()
			{
				string res = "", temp = "";
				res = (!((temp = elem.GetAttribute("innerText")).Equals(""))) ? "\nInnerText =" + temp : "";
				return res;
			}
		}

		#endregion
		// innerHTML

		#region

		private class innerHTML : ExecuteCommand // відкриває браузер
		{
			HtmlElement elem;
			public innerHTML(Tokens.Token Element,Dictionary<string, Tokens.VarInfo> variables)
			{
				foreach (var VARIABLE in variables)
				{
					if (VARIABLE.Key.Equals(Element.value))
					{
						if (!Assignment.IsList(VARIABLE.Value.Var) && !Assignment.IsCollection(VARIABLE.Value.Var))
						{
							elem = ((HtmlElement)VARIABLE.Value.Var);
						}
						else throw new Exception("Error. Invalid using function innerHTML().\n It must be so SinglElement.innerHTML(). Your element is multiple");
					}
				}
			}
			public override dynamic Execute()
			{
				string res = "", temp = "";
				res = (!((temp = elem.GetAttribute("innerHTML")).Equals(""))) ? "\nInnerHTML =" + temp : "";
				return res;
			}
		}

		#endregion
		//Count

		#region

		private class Count : ExecuteCommand
        {
            //просто повертає кількість Pages
            public Count()
            {
            }
            public override dynamic Execute()
            {
				if (Opened)
				{
					return Form2.tabControl.TabCount;
				}
				else
				{
					throw new Exception("Error. Browser isn't opened. Use Open() first");
					return null;
				}
			}
        }

        #endregion

        //ClosePage 

        #region

        private class ClosePage : ExecuteCommand
        {
            private int page_number;

            public ClosePage(Tokens.Token param, Dictionary<string, Tokens.VarInfo> variables)
            {
                if (param.type.Equals(Tokens.Types.Var))
                {
                    var res = variables.Where(i => i.Key.Equals(param.value)).First();
                    page_number = Converter(res.Value.Value,"integer");
                }
                else
                {
                    page_number = Converter(param.value, "integer");
                }
                MessageBox.Show("ClosePage " + page_number);
                page_number--;
            }

            public override dynamic Execute()
            {
				if(Opened)
				{
				Browsers.RemoveAt(page_number);
                Form2.tabControl.TabPages[page_number].Dispose();
                return true;
				}
				else
				{
					throw new Exception("Error. Browser isn't opened. Use Open() first");
					return false;
				}
			}
        }

        #endregion

        //GetElements

        #region

        private class GetElements : ExecuteCommand
        {
            private string Attribute;
            private string Value;
            private int page_number=0;
			private bool Var;
			HtmlElement Element = null;

			public GetElements(Tokens.Token elem, List<Tokens.Token> param, Dictionary<string, Tokens.VarInfo> variables, bool var)
			{
				Var = var;
				Attribute = param[0].value.ToString();
				Value = param[1].value.ToString();
				if (!Var){ 
				if (elem.type.Equals(Tokens.Types.Var))
				{
					var res = variables.Where(i => i.Key.Equals(elem.value)).First();
					page_number = Converter(res.Value.Value, "integer");
				}
				else
				{
					page_number = Converter(elem.value, "integer");
				}
				page_number--;
				}
				else
				{
					foreach(var VARIABLE in variables)
					{
						if(VARIABLE.Key.Equals(elem.value))
                        {
							Element = ((HtmlElement)VARIABLE.Value.Var);
							break;
						}
                    }
				}
                MessageBox.Show("GetElements " + Attribute + ";" + Value);
            }

			public override dynamic Execute()
			{
				if (Opened) {
					if (Attribute.Equals("") || Value.Equals(""))
						throw new Exception("Error. In GetElements function attribute or value is empty!!!");
				if ((page_number < 0 || page_number > Browsers.Count) && Var == false)
				{
					throw new Exception("Error. PageNumber is out of range!");
					return null;
				}
				HtmlDocument htmlDocument = null;
				if (Var)
				{
					if (Element == null)
					{
						throw new Exception("Error. Element is null!!!");
						return null;
					}
				}
				else htmlDocument = Browsers[page_number].Document;
				if (htmlDocument != null || Element != null)
				{
					var selected = new List<HtmlElement>();
					HtmlElementCollection elements = null;
						if (Attribute.Equals("class")) Attribute = "className";
					if (Var) elements = Element.GetElementsByTagName("*");//поверне усі елементи
					else elements = htmlDocument.GetElementsByTagName("*");
					for (int j = 0; j < elements.Count; j++)
					{
						if (elements[j].GetAttribute(Attribute).ToString().Equals(Value))
						{
							selected.Add(elements[j]);
						}
					}
					if (selected.Count > 0)
						return selected;
					else
					{
						selected = null;
						MessageBox.Show("No found elements with such parameters. Check you query or make sure in existence of element by examine webpage");
					}
				}
				else
				{
					MessageBox.Show("Error.There is no Page with number " + page_number);
				} }
				else
				{
					throw new Exception("Error. Browser isn't opened. Use Open() first");
				}
				return null;
            }
        }

        #endregion

        //Open 

        #region

        private class Open : ExecuteCommand // відкриває браузер
        {
            public Open()
            {
                MessageBox.Show("Open function");
				if (Form2 != null)
					Form2.Dispose();
				Form2 = new BrowserF();
				Browsers = new List<WebBrowser>();
            }
            public override dynamic Execute()
            {
				Opened = true;
				Form2.Show();
                Form2.StartPosition = FormStartPosition.Manual;
                Form2.Left = 0;
                Form2.Top = 0;
                Form2.Height = Screen.PrimaryScreen.WorkingArea.Height;
                Form2.Width = Screen.PrimaryScreen.WorkingArea.Width /2;
                Form2.tabControl.TabPages[0].Text = "Tab1";
                Browsers.Add(new WebBrowser());
                Browsers[0].Dock = DockStyle.Fill;
                Form2.tabControl.TabPages[0].Controls.Add(Browsers[0]);
                Browsers[0].Navigate("http://www.google.com");
                while (Browsers[0].ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();
                Browsers[0].Document.Body.Style = "zoom:70%";
                return true;
            }
        }

        #endregion

        //MyExpression

        #region

        private class MyExpression : ExecuteCommand
        {
            private List<Tokens.Token> parametrs = null;
            private string type;
            private List<Tokens.Token> temp;
            private Stack<string> operators;
            public dynamic result;

            public MyExpression(List<Tokens.Token> param, string type, Dictionary<string, Tokens.VarInfo> variables)
            {
                this.parametrs = param;
                this.type = type;
                temp = new List<Tokens.Token>();
                operators = new Stack<string>();
                int i = 0;
				//польський вираз
				if (type.Equals("integer") || type.Equals("fractional")) //якщо тип числовий
				{
					try
					{
						bool Found = false;
						if (param[i].value.Equals("+++") || param[i].value.Equals("---"))//інкремент - декремент
						{
							if (param.Count > i + 3)
							{
								if (param[i + 1].value.Equals("(") &&
								param[i + 3].value.Equals(")"))
								{
									if (param[i + 2].type.Equals(Tokens.Types.Var))
									{
										var Var = variables.Where(it => it.Key.Equals(param[i + 2].value)).First();
										Var.Value.Value = (Converter(Var.Value.Value, Var.Value.Type) + 1).ToString();
										for (int k = i; k < 4; k++)
										{
											param.RemoveAt(i);
										}
										param.Insert(i, new Tokens.Token(Var.Value.Value, Tokens.Types.Value, null, false));
										temp.Add(new Tokens.Token(Var.Value.Value, Tokens.Types.Value, null, false));
										i += 3;
									}
									else
									{
										throw new Exception("Only one parameter for decrement or increment operation!(only integer variable)");
									}
								}
								else
								{
									throw new Exception("Invalid increment or decrement operation!");
								}
							}
							else throw new Exception("Error. Invalid call of operator "+param[i].value);
						}
						while (i < param.Count) //для обчислення
						{
							if (param[i].value.Equals(",")) throw new Exception("Error. Using ',' isn't valid in this part of code!\n For fractional numbers use only '.'");
							if (param[i].type == Tokens.Types.Var) //якщо перший параметр - змінна
							{
								foreach (var VARIABLE in variables)
								{
									if (VARIABLE.Key.Equals(param[i].value))
									//знаходимо її в пулі змінних і беремо її значення
									{
										temp.Add(new Tokens.Token(VARIABLE.Value.Value, Tokens.Types.Value, null, false));
										Found = true;
										break;
									}
								}
								if (!Found)
									throw new Exception("Error.Variable " + param[i].value +
														" wasn't found in the list of Variables");
								//якщо такої змінної немає в списку змінних
							}
							else
								//якщо це якась операція або дужка
							if (param[i].type == Tokens.Types.Operation || param[i].value.Equals("(") || param[i].value.Equals(")"))
							{
								if (operators.Count == 0) //заносимо в стек вперше
								{
									operators.Push(param[i].value);
								}
								else
								{
									if (param[i].value.Equals(")"))
									{
										while (!operators.ElementAt(0).Equals("("))
											//вибираємо всі оператори до ( в стеку
											temp.Add(new Tokens.Token(operators.Pop(), Tokens.Types.Operation,output, false));
										operators.Pop(); //викидаємо дужку
										i++;
										continue;
									}
									if (!param[i].value.Equals("("))
										//викидаємо всі оператори, пріоритет яких є більший або рівний за пріорітет даного
										while (Priority[operators.ElementAt(0)] >= Priority[param[i].value] && operators.Count > 0)
										{
											temp.Add(new Tokens.Token(operators.Pop(), Tokens.Types.Operation,output, false));
											if (operators.Count == 0) break;
										}
									operators.Push(param[i].value); //заносимо даний оператор
								}
							}
							else //додаємо значення
							{
								if (param[i].value.Equals("Pages"))
								{
									if(param.Count>i+4)
									{
										if (param[i + 1].type.Equals(Tokens.Types.Dot) &&
										param[i + 2].value.Equals("Count") &&
										param[i + 3].value.Equals("(") &&
										param[i + 4].value.Equals(")"))
										{
											var list = new List<Tokens.Token>();
											for (int k = 0; k < 5; k++)
											{
												param.RemoveAt(i);
											}
											AllCommands.Add(new Count());
											int res = AllCommands[AllCommands.Count - 1].Execute(); //виконуємо 
											AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
											param.Insert(i, new Tokens.Token(res.ToString(), Tokens.Types.Value, output, false));
											i--;
										}
										else throw new Exception("Error.Invalid call of Count() function");
									}
									else throw new Exception("Error.Invalid call of Count() function");
								}
								else temp.Add(new Tokens.Token(param[i].value, Tokens.Types.Value, output, false));
							}
							i++;
						}
						while (operators.Count > 0) //якщо щось залишилось в стеку
						{							//випихаємо у вираз!
							temp.Add(new Tokens.Token(operators.Pop(), Tokens.Types.Operation, output, false));
						}
						if (temp.Count > 2)
						{
							Info_RTB.Text += "\n=======================\nPolish expression\n";
							for (int k = 0; k < temp.Count; k++)
							{
								Info_RTB.Text += temp[k].value + " ";
							}
						}
					}
					catch(Exception e )
					{
						throw new Exception("Error. Invalid expression! Check structure of expression and try again");
					}
				}
				else if (type.Equals("line")) //якщо тип змінної - string
				{
					result = ""; //виконуємо конкатенацію рядків
					while (i < param.Count)
					{
						if (param.Count > i + 2)
						{
							if (param[i].type.Equals(Tokens.Types.Quotes) &&
						   param[i + 2].type.Equals(Tokens.Types.Quotes))
							{
								result += param[i + 1].value.ToString();
								i += 3;
								continue;
							}
						}
						else throw new Exception("Error. \"is missed or between \"\" is nothing!");
						if (param.Count > i)
						{
							if (param[i].value.Equals("+"))
							{
								i++;
								continue;
							}
							if (param[i].type.Equals(Tokens.Types.Var))
							{
								var Var = variables.Where(x => x.Key.Equals(param[i].value)).First();
								if (param.Count > i + 3)
									if (param[i + 1].type.Equals(Tokens.Types.Dot) && param[i + 2].value.Equals("Value"))
									{
										result += ((HtmlElement)Var.Value.Var).InnerText.ToString();
										i += 3;
									}
									else
									{
										result += Var.Value.Value;
										i++;
									}
								continue;
							}
							else
								throw new Exception("Error.Invalid parameters in assignment Var:=\"\"");
							//якщо у параметрах щось невідоме
						}
						else throw new Exception("Error. There is no parameters!");
					}
				}
				else if (type.Equals("bool"))
				{
					try {
						//отримуємо стрічку виду (_____><=___)
						if (param[0].value.Equals("("))
						{
							i = 1;
							int opened_par = 0, closed, cond_index = 0; //шукаємо закриваючу дужку
							while (!param[i].value.Equals(")") && opened_par != -1)
							{
								if (param.Count > i)
								{
									if (param[i].value.Equals("(")) opened_par++;
									if (param[i].value.Equals(")")) opened_par--;
									if (param[i].value.Equals(">") ||
										param[i].value.Equals("<") ||
										param[i].value.Equals("="))
										if (cond_index == 0)
											cond_index = i;
										else throw new Exception("Error.More than one condition operator (>,<,=)");
									i++;
								}
								else throw new Exception("Error. ')' is missing or statement is invalid");
							}
							closed = i;
							if (cond_index == 0) throw new Exception("Error. Condition operator is missing!");
							dynamic one, two;
							//витягуємо параметри порівняння
							if (cond_index == 2)
							{
								if (param[1].type.Equals(Tokens.Types.Value))
									one = Converter(param[1].value, "fractional");
								else
								{
									var V = variables.Where(e => e.Key.Equals(param[1].value)).First();
									one = Converter(V.Value.Value, "fractional");
								}
								if (closed == 4)
								{
									if (param[3].type.Equals(Tokens.Types.Value))
										two = Converter(param[3].value, "fractional");
									else
									{
										var V = variables.Where(e => e.Key.Equals(param[3].value)).First();
										two = Converter(V.Value.Value, "fractional");
									}
								}
								else
								{
									for (i = cond_index + 1; i < closed; i++)
									{
										temp.Add(param[i]);
									}
									AllCommands.Add(new MyExpression(temp, "fractional", variables));
									two = AllCommands[AllCommands.Count - 1].Execute(); //виконуємо 
									AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
								}
							}
							else
							{
								for (i = 1; i < cond_index; i++)
								{
									temp.Add(param[i]);
								}
								AllCommands.Add(new MyExpression(temp, "fractional", variables));
								one = AllCommands[AllCommands.Count - 1].Execute(); //виконуємо 
								AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
								if (closed == cond_index + 2)
								{
									if (param[3].type.Equals(Tokens.Types.Value))
										two = Converter(param[3].value, "fractional");
									else
									{
										var V = variables.Where(e => e.Key.Equals(param[3].value)).First();
										two = Converter(V.Value.Value, "fractional");
									}
								}
								else
								{
									for (i = cond_index + 1; i < closed; i++)
									{
										temp.Add(param[i]);
									}
									AllCommands.Add(new MyExpression(temp, "fractional", variables));
									two = AllCommands[AllCommands.Count - 1].Execute(); //виконуємо 
									AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
								}
							}

							switch (param[cond_index].value)
							{
								case ">":
									{
										if (one > two) result = true;
										else result = false;
									}
									break;
								case "<":
									{
										if (one < two) result = true;
										else result = false;
									}
									break;
								case "=":
									{
										if (one == two) result = true;
										else result = false;
									}
									break;
								default: throw new Exception("Error. There is no valid conditional operator inside (___)"); break;
							}
						}
						else throw new Exception("Error.Invalid condition without (");
					}
					catch (Exception e )
					{
						throw new Exception("Error inside the statement!");
					}
				}
                   
            }

            public override dynamic Execute()
            {
                int i = 0;
				try
				{
					if (type.Equals("integer") || type.Equals("fractional"))
					{
						if (temp.Count == 1)
						{
							if (type.Equals("integer"))
								return Converter(temp[0].value, "integer");
						}
						string res = "";
						while (temp.Count != 1)
						{
							i = 0;
							while (!temp[i].type.Equals(Tokens.Types.Operation))
							{
								i++;
							}
							switch ((string)temp[i].value)
							{
								case "+":
									{
										res =
											(Converter(temp[i - 2].value, "fractional") +
											 Converter(temp[i - 1].value, "fractional")).ToString();
										temp.Insert(i + 1, new Tokens.Token(res, Tokens.Types.Value, null, false));
										temp.RemoveAt(i - 2); //видаляємо застарілі параметри
										temp.RemoveAt(i - 1);
										temp.RemoveAt(i - 2);
									}
									break;
								case "-":
									{
										res =
											(Converter(temp[i - 2].value, "fractional") -
											 Converter(temp[i - 1].value, "fractional")).ToString();
										temp.Insert(i + 1, new Tokens.Token(res, Tokens.Types.Value, null, false));
										temp.RemoveAt(i - 2); //видаляємо застарілі параметри
										temp.RemoveAt(i - 1);
										temp.RemoveAt(i - 2);
									}
									break;
								case "*":
									{
										res =
											(Converter(temp[i - 2].value, "fractional") *
											 Converter(temp[i - 1].value, "fractional")).ToString();
										temp.Insert(i + 1, new Tokens.Token(res, Tokens.Types.Value, null, false));
										temp.RemoveAt(i - 2); //видаляємо застарілі параметри
										temp.RemoveAt(i - 1);
										temp.RemoveAt(i - 2);
									}
									break;
								case "/":
									{
										res =
											(Converter(temp[i - 2].value, "fractional") /
											 Converter(temp[i - 1].value, "fractional")).ToString();
										temp.Insert(i + 1, new Tokens.Token(res, Tokens.Types.Value, null, false));
										temp.RemoveAt(i - 2); //видаляємо застарілі параметри
										temp.RemoveAt(i - 1);
										temp.RemoveAt(i - 2);
									}
									break;
							}
						}
						if (type.Equals("integer"))
							return Converter(res, "integer");
						else return Converter(res, "fractional");
					}
					if (type.Equals("line"))
					{
						return result.ToString();
					}
					if (type.Equals("bool"))
					{
						return result;
					}
				}
                catch(Exception e)
				{
					throw new Exception("Error. In execution of expression "+temp.Select(x=>x.value).ToString());
				}
                return true;
            }
        }

        #endregion

        //Assignment 

        #region

        private class Assignment : ExecuteCommand
        {
            private List<Tokens.Token> parameters = null;
			private List<Tokens.Token> tokens = null;
			private dynamic result;
            private Dictionary<string, Tokens.VarInfo> variables;
            private Dictionary<string, string> types;
            public static bool IsList(object o)//перевірка об'єкта на тип List
            {
                if (o == null) return false;
                return o is IList &&
                       o.GetType().IsGenericType &&
                       o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
            }
			public static bool IsCollection(object o)//перевірка об'єкта на тип List
			{
				if (o == null) return false;
				if (o.GetType().IsAssignableFrom(typeof(System.Windows.Forms.HtmlElementCollection)))
				{
					return true;
				}
				return false;
			}

			public static String GET_Attribute(HtmlElement elem)
            {
                string result = "\n=======================\n";
                string temp;
                result += (!((temp = elem.TagName).Equals(""))) ? "\nTag =" + temp : "";
                result += (!((temp = elem.GetAttribute("id")).Equals(""))) ? "\nId =" + temp : "";
                result += (!((temp = elem.GetAttribute("name")).Equals(""))) ? "\nName =" + temp : "";
                result += (!((temp = elem.GetAttribute("className")).Equals(""))) ? "\nClass =" + temp : "";
                result += (!((temp = elem.GetAttribute("type")).Equals(""))) ? "\nType =" + temp : "";
                result += (!((temp = elem.GetAttribute("style")).Equals(""))) ? "\nStyle =" + temp : "";
                result += (!((temp = elem.GetAttribute("title")).Equals(""))) ? "\nTitle =" + temp : "";
                result += (!((temp = elem.GetAttribute("src")).Equals(""))) ? "\nSrc =" + temp : "";
                result += (!((temp = elem.GetAttribute("href")).Equals(""))) ? "\nHref =" + temp : "";
                result += (!((temp = elem.GetAttribute("alt")).Equals(""))) ? "\nAlt =" + temp : "";
                result += (!((temp = elem.GetAttribute("width")).Equals(""))) ? "\nWidth =" + temp : "";
                result += (!((temp = elem.GetAttribute("height")).Equals(""))) ? "\nHeight =" + temp : "";
                result += (!((temp = elem.GetAttribute("size")).Equals(""))) ? "\nSize =" + temp : "";
                result += (!((temp = elem.GetAttribute("rows")).Equals(""))) ? "\nRows =" + temp : "";
                result += (!((temp = elem.GetAttribute("cols")).Equals(""))) ? "\nCols =" + temp : "";
              
                return result;
            }
			public static Dictionary<int, string> colors = new Dictionary<int, string> {
				{ 1,"yellow"}, { 2, "gren" }, { 3, "blue" },
				{ 4, "violet" }, { 5, "orange" },{ 6,"lime"},
				{ 7,"aqua"},{ 8,"cyan"},{ 9,"gray"},{ 10,"pink"}};
			public static void SetColor(object elem,string col="")
			{
				try
				{
					if (elem != null)
					{
						string color;
						if (!col.Equals(""))
						{
							color = col;
						}
						else
						{
							Random rnd = new Random();//випадковий вибір кольору
							int numb = rnd.Next(1, 10);
							color = colors[numb];
						}
						if (Assignment.IsList(elem))
						{
							foreach (HtmlElement item in (List<HtmlElement>)elem)
							{
								item.Style = item.Style + ";color:red;background-color:" + color;
							}
						}
						else if (Assignment.IsCollection(elem))
						{
							foreach (HtmlElement item in (HtmlElementCollection)elem)
							{
								item.Style = item.Style + ";color:red;background-color:" + color;
							}
						}
						else ((HtmlElement)elem).Style = ((HtmlElement)elem).Style + ";color:red;background-color:" + color;
					}
				}
				catch(Exception e)
				{
					throw new Exception("Error in function SetColor(color). Invalid parameter");
				}
			}
public static void TypeChecker(string var_key,ref Dictionary<string, Tokens.VarInfo> variables,ref object result,ref object res,ref Dictionary<string, string> types)
			{//res - вхідний параметр (результат функції)
				//result - вихідний параметр (значення змінної типу string)
				var VARIABLE = variables[var_key];
				Info_RTB.Text += "\n\n" + CurrentQuery + "\n";
				if (!VARIABLE.Type.Equals("integer")&&!VARIABLE.Type.Equals("fractional")&&
					!VARIABLE.Type.Equals("bool") && !VARIABLE.Type.Equals("line"))
				{//якщо результат - колекція
					if (IsCollection(res))
					{
						Info_RTB.Text += "\n=======================\n";
						Info_RTB.Text += "\nQuantity = " + ((HtmlElementCollection)res).Count.ToString() + "\n";
						if (VARIABLE.Type.Last().Equals('s'))
						{
							string Type = VARIABLE.Type.Remove(VARIABLE.Type.Length-1, 1);
							bool Equal = true;
							string Collection_type = "";
							foreach (HtmlElement elem in ((HtmlElementCollection)res))
							{
								Info_RTB.Text += GET_Attribute(elem);
								SetColor(elem);
								if (!elem.TagName.Equals(types[Type].ToUpper()) && !elem.TagName[0].Equals(types[Type].ToUpper()))
								{
									Equal = false;
								}
								Collection_type = elem.TagName;
							}
							if (!Equal && !VARIABLE.Value.Equals("Elements"))
							{
								throw new Exception("Error.Type mismatch. Not all elements of collection have type " + types[Type] +
									"\n Change type to " + Collection_type + " or change parametern of function");
								res = null;
							}
						}
						else throw new Exception("Invalid type of variable " + var_key + ". You got collection with type " + ((HtmlElementCollection)res)[0].TagName + ", not one element.\n Use type Elements");
					}
					else// або List або single
					{
						if (IsList(res))
						{
							Info_RTB.Text += "\n=======================\n";
							Info_RTB.Text += "\nQuantity = " + ((List<HtmlElement>)res).Count.ToString() + "\n";
							string Type = VARIABLE.Type;
							if (VARIABLE.Type.Last().Equals('s'))
							{
								Type = VARIABLE.Type;
								Type = Type.Remove(Type.Length - 1, 1);
							}

							bool Equal = true, removed = false;
							for (int index = 0; index < ((List<HtmlElement>)res).Count(); index++)
							{
								HtmlElement elem = ((List<HtmlElement>)res)[index];
								Info_RTB.Text += GET_Attribute(elem);
								SetColor(elem);
								if (!elem.TagName.Equals(types[Type].ToUpper()))
								{
									Equal = false;
									if ((!VARIABLE.Type.Equals("Elements") &&
										!VARIABLE.Type.Equals("Element")) &&
										!VARIABLE.Type[0].Equals(types["Header"].ToUpper()))
									{
										((List<HtmlElement>)res).Remove(elem);//видалення усіх зайвих елементів
										removed = true;
										index--;
									}
								}
							}
							if (((List<HtmlElement>)res).Count() == 0)
							{
								res = null;
							}
							else
							if (removed)
							{
								Info_RTB.Text += "\n/\\/\\/\\/\\/\\/\\/\\/\\/\\/\\\nElements with type of variable:\n";//вивід елементів, що залишились
								foreach (HtmlElement elem in ((List<HtmlElement>)res))
								{
									Info_RTB.Text += GET_Attribute(elem);
									SetColor(elem);//зміна кольору елементів, що залишились
								}
							}
							if (((List<HtmlElement>)res).Count > 1 && !VARIABLE.Type.Last().Equals('s'))
								MessageBox.Show("Warning. Invalid type of variable " + var_key +
". You got list with different elements' types, not one element.\n To avoid this use type Elements. \nType of variable will be implicitly converted to multiple.\n Everything to make your life easier");
						}
						else
						{
							try
							{
									Info_RTB.Text += GET_Attribute((HtmlElement)res);
									SetColor(((HtmlElement)res));
									if (!((HtmlElement)res).TagName[0].Equals(types[VARIABLE.Type].ToUpper()) &&
										!types[VARIABLE.Type].ToUpper().Equals(((HtmlElement)res).TagName.ToString()) && !VARIABLE.Type.Equals("Element"))
									{
										throw new Exception("Error.Type mismatch. " + ((HtmlElement)res).TagName.ToString() + " isn't equal " + VARIABLE.Type.ToString());
										res = null;
									}
							}
							catch(Exception e)
							{
								if (e.Message.First().Equals('E'))
								{
									throw new Exception(e.Message);
								}
								else throw new Exception("Error.Type mismatch. " +res.ToString() + " isn't " + VARIABLE.Type.ToString()+" type ");
							}
							
						}
					}
					variables[var_key].Var = res;
					result = VARIABLE.Var.GetType().ToString();
				}
				else
				{
					switch(VARIABLE.Type.ToString())
					{
						case "integer": {
								variables[var_key].Var = Converter(res, "integer"); 
								result = variables[var_key].Var.ToString();
							} break;
						case "fractional": {
								variables[var_key].Var = Converter(res, "fractional");
								result = variables[var_key].Var.ToString();
							} break;
						case "line": {
								variables[var_key].Var = Converter(res, "line");
								result = variables[var_key].Var.ToString();
							} break;
						case "bool": {
								variables[var_key].Var = Converter(res, "bool");
								result = variables[var_key].Var.ToString();
							} break;
					}
					
				}
			}
            public Assignment(List<Tokens.Token> param, Dictionary<string, Tokens.VarInfo> variables,List<Tokens.Token> tokens)
            {
                types = new Dictionary<string, string> {
                    { "Input", "input" },
                    { "Button", "button" },
                    { "Div", "div" },
                    { "Paragraph", "p" },
                    { "Image", "img" },
                    { "Link", "a" },
                    { "Header", "h" },
                    { "Span", "span" },
                    { "Element", "element" },
                    { "Qtag", "q" },
                    { "TextArea", "textarea" }};
                this.variables = variables;
                this.parameters = param;
				this.tokens = tokens;

                foreach (var VARIABLE in variables) //знаходимо відповідну змінну
                {
                    if (parameters[0].value.Equals(VARIABLE.Key))
                    {
                        //if Pages
                        #region
                        if (param[2].value.Equals("Pages"))
                        {
                            int k = 2;
                            object res = Trees.PagesReturn(ref k, ref param);//повертається результат виклику функцій із початком Pages
                            if (res != null)
                            {
								TypeChecker(VARIABLE.Key,ref variables,ref result, ref res,ref types);//перевіряє типи при присвоєнні і повертає result
								res = null;
							}
                            else
                                result = "null";
                            break;
                        }
						#endregion
						if (param[2].type.Equals(Tokens.Types.Var) && (param[3].value.Equals("[")))//якщо це Element[k]. blabla
						{
							int k = 2;
							object res = Trees.Var_index(ref k,ref tokens,new Tokens.Token(param[2].value,Tokens.Types.Var,null,false));
							if (res != null)
							{
								TypeChecker(VARIABLE.Key, ref variables, ref result, ref res, ref types);
								res = null;
							}
							else
								result = "null";
							break;
						}
						if (param[2].type.Equals(Tokens.Types.Var) && (param[3].value.Equals(".")))//якщо це Element. blabla
						{
							int k = 2;
							object res = Trees.Element_Function(ref k,ref tokens, new Tokens.Token(param[2].value, Tokens.Types.Var, null, false));
							if (res != null)
							{
								TypeChecker(VARIABLE.Key, ref variables, ref result, ref res, ref types);
								res = null;
							}
							else
								result = "null";
							break;
						}
						if (parameters.Count() > 4) //якщо після :=не один параметр
                        {
                            var Var = new Tokens.Token(param[0].value, param[0].type, null, false);
                            var Eq = new Tokens.Token(param[1].value, param[1].type, null, false);
                            param.Remove(param.First()); //видаляємо Var:=
                            param.Remove(param.First());
                            //присвоєння стрічки
                            if (param[0].type.Equals(Tokens.Types.Quotes) &&
                                param[2].type.Equals(Tokens.Types.Quotes) &&
                                param[3].type.Equals(Tokens.Types.Semicolon))
                            {
                                result = param[1].value;
                                param.Insert(0, Eq);
                                param.Insert(0, Var);
                            }
                            else
                            {
                                //якщо там щось складніше - кидаємо дальше ( самі токени, який тип має повернути, вказівка на повернення значення)
                                if (param[param.Count - 1].type.Equals(Tokens.Types.Semicolon))
                                {
                                    param.Remove(param[param.Count - 1]);
                                }
                                else
                                {
                                    throw new Exception("Error.; is missed after assignment!");
                                }
                                AllCommands.Add(new MyExpression(param, VARIABLE.Value.Type, variables));
                                param.Insert(0, Eq);
                                param.Insert(0, Var);
                                result = AllCommands[AllCommands.Count - 1].Execute();
                                AllCommands.Remove(AllCommands[AllCommands.Count - 1]);
                            }
                        }
                        else
                        {
                            //якщо тільки один параметр
                            if(param[2].type.Equals(Tokens.Types.Value))
                            result = param[2].value;
                            else
                            if (param[2].type.Equals(Tokens.Types.Var))
                            {
                                foreach(var VAR in variables)
                                {
                                    if(VAR.Key.Equals(param[2].value))
                                    {
                                        result = VAR.Value.Value.ToString();
                                        break;
                                    }
                                }
                            }   
                        }
						break;
                    }
                }
            }

            public override dynamic Execute()
            {
                foreach (var VARIABLE in variables) //знаходимо відповідну змінну
                {
                    if (parameters[0].value.Equals(VARIABLE.Key))
                    {
						object temp=null;
						if(VARIABLE.Value.Var!=null && !VARIABLE.Value.Type.Equals("integer") && !VARIABLE.Value.Type.Equals("fractional") && !VARIABLE.Value.Type.Equals("line"))
						TypeChecker(VARIABLE.Key,ref variables,ref temp,ref VARIABLE.Value.Var, ref types);//перевірка на співпадіння типів і повернення результату
						else TypeChecker(VARIABLE.Key, ref variables, ref temp, ref result, ref types);
						VARIABLE.Value.Value = Convert.ToString((Converter(temp, VARIABLE.Value.Type)));
                        Grid.Rows[Grid.Rows.Count-1].Cells[2].Value = Convert.ToString(VARIABLE.Value.Value);
                    }
                }
                return true;
            }
        }

        #endregion

        //public static dynamic Converter(dynamic variable, string type)

        #region

        public static dynamic Converter(dynamic variable, string type)
        {
            string str = Convert.ToString(variable);
            switch (type)
            {
                case "integer":
                    {
                        int res_converting;
                        if (Int32.TryParse(str, out res_converting))
                        {
                            variable = res_converting;
                        }
                        else
                        {
                            MessageBox.Show("Warning .Type should be integer!");
                            str = str.Replace(".", ",");
                            double res = 0.0;
							if (Double.TryParse(str, out res))
							{
								int temp = (int)res;
								variable = temp;
							}
							else throw new Exception("Error. You can't assign "+str +" to integer variable");
                        }

                    }
                    break;
                case "fractional":
                    {
                        double res_converting;
                        str = str.Replace(".", ",");
                        if (Double.TryParse(str, out res_converting))
                        {
                            variable = res_converting;
                        }
                        else
                        {
                            MessageBox.Show("Warning . Type should be fractional!");
                            if (variable is Int32)
                            {
                                double temp = Int32.Parse(variable);
                                variable = temp;
                            }
							else throw new Exception("Error. You can't assign " + str + " to fractional variable");
						}

                    }
                    break;
                case "line":
                    {
                        variable = Convert.ToString(variable);
                    }
                    break;
                case "bool":
                    {
                        bool res_converting;
                        if (Boolean.TryParse(variable, out res_converting))
                        {
                            variable = res_converting;
                        }
                        else throw new Exception("Error. Invalid type. It must be bool");
					}
                    break;
				default:
					{

					}break;
            }
            return variable;
        }

        #endregion

        //abstract class Command

        #region

        internal abstract class Command
        {

            public virtual dynamic Execute()
            {
                return 0;
            }
        }

        #endregion

        //ExecuteCommand

        #region

        public class ExecuteCommand : Command
        {
            public Tokens.Token NextToken;
        }

        #endregion


    }
}
