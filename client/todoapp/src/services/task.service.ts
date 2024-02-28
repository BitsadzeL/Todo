import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskServiceService {
  private apiURL='https://localhost:7226/Task';



  constructor(private http: HttpClient) { }


  getAllTasks(): Observable<any> {
    return this.http.get<any>(this.apiURL+'/GetAllTasks');
  }

  deleteTask(id:number): Observable<any>{
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.delete(`${this.apiURL}/DeleteTask/${id}`, { headers });
  }

  addNewTask(task:any):Observable<any>{
    const url = `${this.apiURL}/AddTask`;
    return this.http.post(url, task);
  }

  updateTaskStatus(taskId: number, status: string): Observable<any> {
    const url = `${this.apiURL}/UpdateTaskStatus/${taskId}`;
    const newStatus = status === 'Active' ? 'Completed' : 'Active';
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put(url, `"${newStatus}"`, { headers });
  }
  
  
  

}
