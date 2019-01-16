  import * as React from 'react';
  import *as ReactDOM from 'react-dom';
  import ActivitiesService from '../services/ActivitiesService';
  import TableActivities from './TableActivities';

  /*//////////////////////////////////
  //State Components
  *///////////////////////////////////

  export default class ModalComponent extends React.Component<any, any>{
    service: ActivitiesService;
    initialize: TableActivities;
    constructor(props: any) {
      super(props);
      this.state = {  
        name: this.props.item.name,
        type: +this.props.item.type
      }
      this.NameChange = this.NameChange.bind(this);
      this.showModal = this.showModal.bind(this);
      this.service = new ActivitiesService();
      this.initialize = new TableActivities(props);
    }

    editActivity(activity: any) {
      console.log(activity)
      activity.name = this.state.name
      activity.typeId = +this.state.type
      this.service.editActivity(activity)
        .then((res: any) => {
            this.props.initializeTable    
        });
    }

    NameChange = (e: any) => {
      this.setState({ name: e.target.value });
      console.log(this.state.name);
    }

    TypeChange = (e: any) => {
      this.setState({ type: e.target.value });
      console.log(this.state.type);
    }

    showModal() {
      //Shows the hidden Modal
      ReactDOM.findDOMNode(this.refs.modal);
    }

    render() {
      return (
        <div>
          <button
            onClick={this.showModal}
            type="button"
            className="editButton"
            data-toggle="modal"
            data-target="#myModal">Edit
          </button>

          <div className="modal fade" id="myModal" role="dialog">
            <div className="modal-dialog">
              <div className="modal-content modalBack">
                <div className="modal-header">
                  <button
                    type="button"
                    className="close"
                    data-dismiss="modal"
                    style={{ color: "#eee" }}>
                    &times;
                  </button>

                  <h4>Editing</h4>
                </div>
                <div className="modal-body">
                  {this.props.content}
                  <form action="">
                    <h1>Type</h1>:<label htmlFor="">type</label>
                    <label htmlFor=""></label>
                    <input type="text" value={this.state.name || ""} onChange={this.NameChange} style={{ color: "#1f2933" }} />
                    <label htmlFor=""></label>
                    <input type="number" value={this.state.type || ""} onChange={this.TypeChange} style={{ color: "#1f2933" }} />
                  </form>
                </div>
                <div className="modal-footer">
                  <input
                    type="button"
                    className="btn btn-default"
                    data-dismiss="modal"
                    value="Update"
                    onClick={() => this.editActivity(this.props.item)}
                  />

                  <button
                    type="button"
                    className="btn btn-default"
                    data-dismiss="modal">
                    Close
                  </button>
                </div>
              </div>
            </div>
          </div>

        </div>
      );
    }
  }
