import * as React from 'react';
import *as ReactDOM from 'react-dom';
import ActivitiesService from '../services/ActivitiesService';

/*//////////////////////////////////
//State Components
*///////////////////////////////////

var createMode = "create";
var updateMode = "update";

export default class ModalComponent extends React.Component<any, any>{
  service: ActivitiesService;

  constructor(props: any) {
    super(props);
    debugger;
    if (this.props.item) {
      this.state = {
        mode: updateMode,
        name: this.props.item.name,
        typeId: +this.props.item.type.id,
        types: []
      }
    }
    else {
      this.state = {
        mode: createMode,
        name: "",
        typeId: 0,
        types: []
      }
    }

    this.NameChange = this.NameChange.bind(this);
    this.showModal = this.showModal.bind(this);
    this.service = new ActivitiesService();
  }

  editActivity(activity: any) {
    debugger;
    activity.name = this.state.name;
    activity.typeId = +this.state.typeId;
    this.service.editActivity(activity)
      .then((res: any) => {
        this.props.initializeTable()
      });
  }

  createActivity() {
    var activity = {
      Name: this.state.name,
      TypeId: +this.state.typeId
    }

    this.service.addActivity(activity)
      .then((res: any) => {
        this.props.initializeTable()
      });
  }

  // componentWillReceiveProps(props: any) {
  //   if (props.item) {
  //     this.setState({
  //       mode: updateMode,
  //       name: props.item.name,
  //       typeId: props.item.type,
  //       types: []
  //     });
  //   }
  // }

  componentDidMount() {
    this.service.getAllActivityTypes()
      .then((res: any) => {
        this.setState({
          types: res.data,
          typeId: res.data[0].id
        });
      })
  }

  onSubmit(activity: any) {
    if (activity) {
      this.editActivity(activity);
    }
    else {
      this.createActivity();
    }
  }

  NameChange = (e: any) => {
    this.setState({ name: e.target.value });
  }

  TypeChange = (e: any) => {
    debugger;
    this.setState({ typeId: e.target.value });
  }

  showModal() {
    // //Shows the hidden Modal
    ReactDOM.findDOMNode(this.refs.modal);
  }

  getModalHeader() {
    return this.state.mode === createMode ? "Create" : "Edit";
  }

  getBtnText() {
    return this.state.mode === createMode ? "Create" : "Edit";
  }

  getBtnStyles() {
    return this.state.mode === createMode ?
      "btn btn-success" :
      "btn btn-success btn-block";
  }

  render() {
    return (
      <div>
        <button
          onClick={this.showModal}
          type="button"
          className="btnTable btnEdit"
          data-toggle="modal"
          data-target={"#myModal" + this.props.id}>{this.getBtnText()}
        </button>
        <div className="modal fade" id={"myModal" + this.props.id} role="dialog">
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

                <h4>{this.getModalHeader()}</h4>
              </div>
              <div className="modal-body">
                <form action="">
                  <div className="form-group">
                    <label htmlFor="activityName">Name</label>
                    <input type="text" value={this.state.name || ""}
                      className="form-control"
                      onChange={this.NameChange}
                      name="activityName"
                      style={{ color: "#1f2933" }} />
                  </div>
                  <div className="form-group">
                    <label htmlFor="reportMinutes">Actvity type</label>
                    <select className="form-control" value={this.state.typeId} onChange={this.TypeChange}>
                      {this.state.types.map((a: any) =>
                        <option value={a.id} key={a.id}>
                          {a.name}
                        </option>
                      )}
                    </select>
                  </div>
                </form>
              </div>
              <div className="modal-footer">
                <input
                  type="button"
                  className="btn btn-default"
                  data-dismiss="modal"
                  value="Update"
                  onClick={() => this.onSubmit(this.props.item)}
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
