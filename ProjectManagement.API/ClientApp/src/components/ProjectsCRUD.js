import React, {useEffect, useState} from 'react';
import {Link} from "react-router-dom";
import axios from "axios";

const ProjectsCRUD = () => {
    const [loading, setLoading] = useState(true)
    const [data, setData] = useState([]);

    useEffect(() => {
        try {
            const fetchData = async () => {
                const {data} = await axios('api/projects');
                setData(data.result)
                setLoading(false)
            }
            fetchData().catch((e) => console.log())
        } catch (e) {
            console.log(e.message)
            setLoading(false)
        }
    }, [])

    return (<div>
            {loading ? <p><em>Loading...</em></p> :
                <table className='table table-striped text-center' aria-labelledby="tabelLabel">
                    <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Actions</th>
                    </tr>
                    </thead>
                    <tbody>
                    {data.map((project, index) => <tr key={index}>
                        <td>{project.id}</td>
                        <td><Link tag={Link} className="text-dark"
                                  to={`/getById/${project.id}`}>{project.name}</Link></td>
                        <td>
                            <button className="btn btn-primary px-4 me-2">Edit</button>
                            <button className="btn btn-danger px-4 ms-2">Delete</button>
                        </td>
                    </tr>)}
                    </tbody>
                </table>}
        </div>);
}

export default ProjectsCRUD;