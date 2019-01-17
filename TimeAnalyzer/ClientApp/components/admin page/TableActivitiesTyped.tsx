import * as React from 'react';
import ActivitiesService from '../services/ActivitiesService';

export default class TableActivityTypes extends React.Component<any, any>{

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

        this.service.getAllActivityTypes()
            .then((res: any) => {
                this.setState({
                    activities: res.data
                });
            });

    }

    deleteActivityType(activityType: any) {
        this.service.deleteActivityType(activityType)
            .then((res: any) => {
                this.setState({
                    activities: this.state.activities.filter((at: any) => at.id !== activityType.id)
                });
            });
    }

    render() {
        return (
            <div style={{border: "1px solid #eee", marginTop: "1em", paddingBottom: "1em"}}>
                <label className="tableLabels">Activity Types</label>
                <div className="scrolltable style-scroll">
                    <table className='table '>

                        <thead>
                            <tr>
                                <th className="col-md-3">Name</th>
                                <th className="col-md-3">Importance factor</th>
                                <th className="col-md-3">Type Color</th>
                            </tr>

                        </thead>
                        <tbody>
                            {

                                this.state.activities.map((item: any) => {
                                    return (
                                        <tr key={item.id}>
                                            <td className="col-md-3">{item.name}</td>
                                            <td className="col-md-3">{item.importanceFactor}</td>
                                            <td className="col-md-3" style={{ background: item.colorValue }}>{item.colorValue}</td>
                                        </tr>
                                    )
                                })
                            }
                        </tbody>
                    </table>
                </div>
            </div>

        )


    }



}