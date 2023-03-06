import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Course } from 'src/app/models/course';
import { CodingCoursesService } from 'src/app/services/coding-courses.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { TopicsModalComponent } from '../topics-modal/topics-modal.component';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent {

  editMode: boolean = false;
  dataSource: MatTableDataSource<Course> = new MatTableDataSource();
  displayedColumns: string[] = ['name', 'description', 'actions'];

  newCourse: Course = new Course();

  constructor(
    private codingCoursesService: CodingCoursesService,
    private dialog: MatDialog) {}

  ngOnInit() : void {
    this.loadResults();
  }

  private loadResults() {
    this.codingCoursesService.getCourses().subscribe((results: Course[]) => this.dataSource = new MatTableDataSource(results));
  }

  remove(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: 'Czy na pewno chcesz usunąć kurs?',
    });
  
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.codingCoursesService.removeCourseById(id).subscribe(() => this.loadResults());
      }
    });
  }

  save(course: Course) {
    this.codingCoursesService.updateCourse(course).subscribe(() => {
      course.editMode = !course.editMode;
      this.loadResults();
    });
  }

  add() {
    this.codingCoursesService.addCourse(this.newCourse).subscribe(() => {
      this.loadResults();
      this.newCourse = new Course();
    });
  }

  showTopicsModal(course: Course) {
    const dialogRef = this.dialog.open(TopicsModalComponent, {
      width: '50vw',
      data: course,
    });

    dialogRef.componentInstance.reloadDataEvent.subscribe(() => {
      this.loadResults();
    });
  }
}
