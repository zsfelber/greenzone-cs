using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
public class Position
{
	public int Line;

	public int Column;
	internal Position()
	{
		this.Line = 0;
		this.Column = 0;
	}

	internal void Copy(Position Pos)
	{
		this.Column = Pos.Column;
		this.Line = Pos.Line;
	}
}
