import { Component } from '@angular/core';
import { Task } from 'src/interfaces/task';
import { TaskServiceService } from 'src/services/task.service';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  constructor(private service: TaskServiceService) { }
  tasks: Task[] = []

  newTask: string = '';

  ngOnInit(): void {
    this.fetchTasks();
  }

  onDeleteTask(taskID: number): void {
    this.service.deleteTask(taskID).subscribe(
      () => {
        console.log('Task deleted successfully');

        this.fetchTasks();
      },
      (error) => {
        console.error('Error deleting task', error);
      }
    );
  }

  onAddTask(task: any) {
    const taskToAdd = {
      description: this.newTask,
      completed: "false"
    };
    this.service.addNewTask(taskToAdd).subscribe(
      response => {
        console.log('Task added successfully:', response);

        this.newTask = ''
        this.fetchTasks();
      },
      error => {
        console.error('Error adding task:', error);
      }
    );

  }

  fetchTasks() {
    this.service.getAllTasks().subscribe(data => {
      this.tasks = data;
    });
  }


}
