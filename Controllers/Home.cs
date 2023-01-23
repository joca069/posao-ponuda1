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



public class HomeController:Controller{
	
	public string Index(){
	
	
	Response.Redirect("/employees");
	return "test";
	}

}

