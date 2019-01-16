import * as React from 'react';
import UsersService from '../services/UsersService';

export default class TableUsers extends React.Component<any, any>{

    userService: UsersService;
    constructor(props: any) {
        super(props)
        this.state = {
            users: []
        }
        this.userService = new UsersService();
    }

    componentDidMount() {

        this.userService.getAllUsers()
            .then((res: any) => {
                this.setState({
                    users: res.data
                });
            });
    }

    deleteUser(user: any) {
        this.userService.deleteUser(user)
            .then((res: any) => {
                this.setState({
                    users: this.state.users.filter((u: any) => u.id !== user.id)
                });
            });
    }

    render() {
        return (
            <div style={{border: "1px solid #eee", marginTop: "1em", paddingBottom: "1em"}}>
                <label className="tableLabels">Users</label>
                <div className="scrolltable style-scroll">
                    <table className='table'>

                        <thead>
                            <tr>
                                <th className="col-md-4">Name</th>
                                <th className="col-md-4">Email</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.state.users.map((item: any) => {
                                    return (
                                        <tr key={item._id}>
                                            <td className="col-md-4">{item.name}</td>
                                            <td className="col-md-4">{item.email}</td>
                                            <td className="col-md-4" style={{background: "none"}}>
                                                <button className="deleteButton" onClick={() => this.deleteUser(item)}>
                                                    delete
                                                </button>
                                            </td>
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