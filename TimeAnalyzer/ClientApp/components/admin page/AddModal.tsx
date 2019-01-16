import * as React from 'react';
import *as ReactDOM from 'react-dom';
import ActivitiesService from '../services/ActivitiesService';

/*//////////////////////////////////
//State Components
*///////////////////////////////////

export default class AddModalComponent extends React.Component<any, any>{
  service: ActivitiesService;
  constructor(props: any) {
    super(props);
    this.state = {
      name: "",
      type: ""
    }
    this.NameAdd = this.NameAdd.bind(this);
    this.TypeAdd = this.TypeAdd.bind(this);

    this.service = new ActivitiesService();
  }

  addActivity(name:any, typeId:any){
    name = this.state.name
    typeId = +this.state.type
    this.service.addActivity(name,typeId)
      .then((res: any) => {
             
      });
  }

  NameAdd = (e: any) => {
    this.setState({ name: e.target.value });
    console.log(this.state.name);
  }

  TypeAdd = (e: any) => {
    this.setState({ type: e.target.value });
    console.log(this.state.type);
  }



  render() {
    return (
      <div>
        

      </div>
    );
  }
}
