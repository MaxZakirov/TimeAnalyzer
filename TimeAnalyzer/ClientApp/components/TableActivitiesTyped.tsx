import * as React from 'react';
import ActivitiesService from './services/ActivitiesService';

export default class TableActivitiesTyped extends React.Component<any, any>{

    service: ActivitiesService;
    constructor(props: any) {
        super(props)
        this.state = {
            data: []
        }
        this.service = new ActivitiesService();
    }    

     componentDidMount() {

         this.service.getAllActivities()
         .then((res: any) => {
             this.setState({
                 data: res.data
             });
         });
         
     }

     render() {
        const { data } = this.state
        return (
            <div>
                <label className="tableLabels">ActivityType</label>
                <div className="scrolltable">
                    <table className='table table-bordered table-striped'>

                        <thead>
                            <tr>
                                <th className="col-md-6">Name</th>
                                <th className="col-md-6">ActivityType</th>
                            </tr>
                            
                        </thead>
                        <tbody>
                            {
                                
                                data.map((item: any) => {
                                    return (
                                        <div>
                                            <tr key={item._id}>
                                                <td className="col-md-6">{item.activity.name}</td>
                                                <td className="col-md-6">{item.activity.colorValue}</td>
                                            </tr>
                                        </div>

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