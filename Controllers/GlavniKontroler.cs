
using Microsoft.AspNetCore.Mvc; 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace _.Controllers;



public class employeesController:Controller{



	public  IActionResult Index()
	{
	
	SqlConnection connection=new SqlConnection("Database=posao;Server=localhost;user=SA;password=jocacar.123");
	connection.Open();

	SqlCommand command = new SqlCommand("select count(id) from employees ", connection);
	SqlDataReader reader = command.ExecuteReader();
	int size=0;

	while(reader.Read()){
	ViewData["size"]=reader.GetInt32(0);
	size=reader.GetInt32(0);
	}
	
	reader.Close();


	string sql="select ime,prezime,adresa,pozicija,bruto_plata from employees ";
	command = new SqlCommand(sql, connection);
	reader = command.ExecuteReader();
	int i=0;
	

	string[,] args=new string[size,5];

	while (reader.Read()){
	
	
	for(int o=0;o<4;o++){
	args[i,o]=reader.GetString(o);
	}
	
	args[i,4]=""+reader.GetInt32(4);
	i++;
	}

	ViewData["args"]=args;
	return View();
	
	}

	public IActionResult insert()
	{
		return View();
	}


	// pulls out parameter from POST body
	private string readParametar(string name,string body)
	{
	
	int start=body.IndexOf(name)+name.Length+1;
	int end=body.IndexOf('&',start);
	if(end==-1){
	end=body.Length;
	}
	return body.Substring(start,end-start);
	}

	[HttpPost]
	public async Task<string> addEmployee()
	{

		var body = await new StreamReader(Request.Body).ReadToEndAsync();
		
		SqlConnection connection=new SqlConnection("Database=posao;Server=localhost;user=SA;password=jocacar.123");
		connection.Open();
		
		string sql="insert into employees(ime,prezime,pozicija,adresa,bruto_plata) values ('"+readParametar("ime",body)+"','"+readParametar("prezime",body)+"','"+readParametar("pozicija",body)+"','"+readParametar("adresa",body)+"',"+readParametar("bruto_plata",body)+" )";
		//Console.Write(sql+"\n\n\n");
		SqlCommand command = new SqlCommand( sql,connection);
		SqlDataReader reader = command.ExecuteReader();

		//redirects client to main table 
		Response.Redirect("/employees");
		

		return body+"\n"+readParametar("bruto_plata",body);
	}
	

	public IActionResult details(string? id)	
	{
	
	if(id == null){
	Response.Redirect("/employees");
	}
	
	string[] ime_prezime=id.Split("_");


	ViewData["ime"]=ime_prezime[0];

	ViewData["prezime"]=ime_prezime[1];
	

	SqlConnection connection=new SqlConnection("Database=posao;Server=localhost;user=SA;password=jocacar.123");
	connection.Open();

	string sql="select top 1 * from employees where ime like '"+ime_prezime[0]+"' and prezime like '"+ime_prezime[1]+"' ";
	//Console.Write(sql+"\n\n\n");

	SqlCommand command = new SqlCommand( sql,connection);
	SqlDataReader reader = command.ExecuteReader();

	while(reader.Read()){
	
	ViewData["ime"]=reader.GetString(1);
	ViewData["prezime"]=reader.GetString(2);
	ViewData["adresa"]=reader.GetString(3);
	ViewData["bruto"]=reader.GetInt32(4);
	}

	return View();
}

}



