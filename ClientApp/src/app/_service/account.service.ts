import { HttpClient } from "@angular/common/http";
import { Inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { User } from "../_model/User";
import { Observable,  catchError } from "rxjs";


@Injectable({
    providedIn: 'root'
  })
  export class AuthService {
    
    public loggedUser: User | null;
  
    constructor(
      private router: Router,
      private http: HttpClient,
      @Inject('BASE_URL') 
      private baseUrl: string,
    ) {
        this.loggedUser= null;
     }

  
    login(login: string, password: string) {
      return this.http.post(this.baseUrl +`login`, {login, password}, {observe : 'response'})
      .pipe(catchError(error => {
        const statusCode = error.status;
        if(statusCode == 400){
            return (error);
        }
      }))
      .subscribe(response=>{
         this.loggedUser = JSON.parse(response as string);
        console.log(response);
      })
    }
  
    logout() {
        return this.http.post(this.baseUrl +`logout`, '').pipe(
            catchError(error => {
                const statusCode = error.status;
                console.log(error.message);
                return (error);
            })
        )
            .subscribe(resp => {
                this.deleteUser();
                this.router.navigate(['/login']);
            })
    }
    
    deleteUser()
    {
        localStorage.removeItem('user');
    }
  }