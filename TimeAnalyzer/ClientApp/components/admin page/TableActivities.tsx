import * as React from 'react';
import ActivitiesService from '../services/ActivitiesService';
import * as $ from 'jquery'
import ModalComponent from './EditModal';
import AddModalComponent from './AddModal';


export default class TableActivities extends React.Component<any, any>{

    service: ActivitiesService;
    constructor(props: any) {
        super(props)
        this.state = {
            data: [],
            activities: []
        }
        this.service = new ActivitiesService();
    }

    componentDidMount() {
        this.service.getAllActivities()
            .then((res: any) => {
                this.setState({
                    activities: res.data
                });
            });
    }

    initializeTable() {
        this.service.getAllActivities()
            .then((res: any) => {
                this.setState({
                    activities: res.data
                });
            });
    }

    deleteActivity(activity: any) {
        this.service.deleteActivity(activity)
            .then((res: any) => {
                this.setState({
                    activities: this.state.activities.filter((a: any) => a.id !== activity.id)
                });
            });
    }

    render() {

        return (
            <div style={{border: "1px solid #eee", marginTop: "1em", paddingBottom: "1em"}}>

                <label className="tableLabels">Activities</label>
                <div className="scrolltable style-scroll">
                    <table className='table'>

                        <thead>
                            <tr>
                                <th className="col-md-4">Name</th>
                                <th className="col-md-4">Activity type</th>
                            </tr>

                        </thead>
                        <tbody>
                            {
                                this.state.activities.map((item: any) => {
                                    return (
                                        <tr key={item._id}>
                                            <td className="col-md-3">{item.name}</td>
                                            <td className="col-md-3">{item.type.name}</td>
                                            <td className="col-md-3 btnTable" style={{ background: "none" }}>
                                                <ModalComponent item={item} initializeTable={this.initializeTable()} />
                                            </td>
                                            <td className="col-md-3 btnTable" style={{ background: "none" }}>
                                                <button className="deleteButton" onClick={() => this.deleteActivity(item)}>
                                                    Delete
                                                </button>
                                            </td>
                                        </tr>
                                    )
                                })
                            }
                        </tbody>
                    </table>
                </div>
                <td className="col-md-3 addButton">
                        <AddModalComponent initializeTable={this.initializeTable()}/>
                </td>
            </div>

        )


    }



}