import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import axiosApi from "../axiosApi";
import Button from "react-bootstrap/Button";

const ProjectDetails = () => {
    const [loading, setLoading] = useState(true)
    const [data, setData] = useState();
    const id = useParams().id;
    const [executors, setExecutors] = useState([]);
    const [showResults, setShowResults] = useState(false);

    useEffect(() => {
        try {
            const fetchData = async () => {
                const {data} = await axiosApi(`projects/getById/${id}`);
                setData(data.result)
                setLoading(false)
            }
            fetchData().catch((e) => console.log())
        } catch (e) {
            console.log(e.message)
            setLoading(false)
        }
    }, [])

    const showExecutors = async (id) => {
        const result = await axiosApi(`employees/getExecutorsByProjectId/${id}`);
        setExecutors(result.data.result);
        setShowResults(!showResults);
    }

    return (
        <div>
            {loading ? <p><em>Loading...</em></p> :
                <div className="card mb-3">
                    <div className="card-body">
                        <h5 className="card-title">{data.name}</h5>
                        <p className="card-text">Client company name: {data.clientCompanyName}</p>
                        <p className="card-text">Executor company name: {data.executorCompanyName}</p>
                        <p className="card-text">Priority: {data.priority}</p>
                        <p className="card-text">Manager Id: {data.managerId}</p>
                        {showResults ?
                            <div id="executorsBlock">
                                {executors.length === 0 ? <span>No executors yet.</span> :
                                    <div>Executors:<br/>
                                        {
                                            executors.map((employee) =>
                                                <span
                                                    key={employee.id}>{employee.name} {employee.surname} (id: {employee.id})<br/></span>)
                                        }
                                    </div>}
                            </div> : null
                        }
                        <Button className="card-text" onClick={() => showExecutors(data.id)}>
                            {showResults ? <span>Hide</span> : <span>Show</span>} executors</Button>
                        <div className="card-text"><small className="text-muted">Creation
                            date: {new Date(data.startDate).toString()}</small></div>
                    </div>
                </div>
            }
        </div>
    );
};

export default ProjectDetails;