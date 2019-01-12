import * as React from "react"

export default class UserDeleteButton extends React.Component<any,any>{

    deleteUser(){
        return null
    }
    render(){
        return(
            <button className="deleteButton" onClick={this.deleteUser}>
                delete
            </button>
        )
    }
}