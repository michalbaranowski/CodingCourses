import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Course } from 'src/app/models/course';
import { Topic } from 'src/app/models/topic';
import { CodingCoursesService } from 'src/app/services/coding-courses.service';

@Component({
  selector: 'app-topics-modal',
  templateUrl: './topics-modal.component.html',
  styleUrls: ['./topics-modal.component.css']
})
export class TopicsModalComponent {
    dataSource: MatTableDataSource<Topic> = new MatTableDataSource();
    displayedColumns: string[] = ['orderNumber', 'description', 'actions'];
    course: Course;

    newTopic: Topic = new Topic();

    @Output() reloadDataEvent = new EventEmitter<void>();

    constructor(
      @Inject(MAT_DIALOG_DATA) public data: Course,
      public dialogRef: MatDialogRef<TopicsModalComponent>,
      private codingCoursesService: CodingCoursesService
    ) {
      this.course = data;

      let sortedData = this.sortByOrder(data.topics);
      this.dataSource = new MatTableDataSource(sortedData);
    }
  
    onClick(result: boolean): void {
      this.dialogRef.close(result);
    }

    removeTopic(id: number) {
      this.course.topics = this.course.topics.filter(x => x.id !== id);
      this.dataSource = new MatTableDataSource(this.course.topics);
      
      this.codingCoursesService.updateCourse(this.course).subscribe(() => this.reloadDataEvent.emit());
    }

    changeValue(topic: Topic) {
      this.course.topics.filter(x => x.id === topic.id)[0].value = topic.value;

      let sortedData = this.sortByOrder(this.course.topics);
      this.dataSource = new MatTableDataSource(sortedData);

      this.codingCoursesService.updateCourse(this.course).subscribe(() => this.reloadDataEvent.emit());
    }

    add() {
      this.course.topics.push(this.newTopic);
      let sortedData = this.sortByOrder(this.course.topics);
      this.dataSource = new MatTableDataSource(sortedData);
      this.newTopic = new Topic();

      this.codingCoursesService.updateCourse(this.course).subscribe(() => this.reloadDataEvent.emit());
    }

    private sortByOrder(topics: Topic[]) : Topic[] {
      return topics.sort((a,b) => {
        return a.orderNumber < b.orderNumber ? -1 : 1
      });
    }
}
