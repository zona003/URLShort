import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Url } from "../_model/Url";
import { Observable } from "rxjs";

@Injectable()
export class DataService{
    private apiurl = "/api/urls";

    constructor(private http: HttpClient){

    }

    getUrls(){
        return this.http.get<Url[]>(this.apiurl);
    }

    getUrl(id: number){
        return this.http.get<Url>(this.apiurl+"/"+ id);
    }

    createShortUrl(longUrl: string)
    {
        return this.http.post(this.apiurl, longUrl);
    }

    deleteUrl(id : number){
        return this.http.delete(this.apiurl + "/" + id)
    }
}