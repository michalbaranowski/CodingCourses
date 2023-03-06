import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environments';
import { Course } from '../models/course';

@Injectable({
  providedIn: 'root'
})
export class CodingCoursesService {

  private url = "courses";

  constructor(private http: HttpClient) { }

  public getCourses() : Observable<Course[]> {
    return this.http.get<Course[]>(`${environment.apiUrl}${this.url}`);
  }

  public removeCourseById(id: number) : Observable<void> {
    const url = `${environment.apiUrl}${this.url}?id=${id}`;
    return this.http.delete<void>(url);
  }

  public addCourse(course: Course) : Observable<void> {
    const url = `${environment.apiUrl}${this.url}`;
    return this.http.post<void>(url, course);
  }

  public updateCourse(course: Course) : Observable<void> {
    const url = `${environment.apiUrl}${this.url}`;
    return this.http.put<void>(url, course);
  }
}
