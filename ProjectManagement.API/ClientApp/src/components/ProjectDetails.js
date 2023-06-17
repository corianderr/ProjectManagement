import React, {useEffect, useState} from 'react';
import {useParams} from "react-router-dom";
import axiosApi from "../axiosApi";

const ProjectDetails = () => {
    const [loading, setLoading] = useState(true)
    const [data, setData] = useState();
    const id = useParams().id;

    useEffect(() => {
        try {
            const fetchData = async () => {
                const {data} = await axiosApi.get(`projects/getById/${id}`);
                setData(data.result);
                setLoading(false);
            }
            fetchData();
        } catch (e) {
            console.log(e.message)
            setLoading(false)
        }
    }, [])


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
                        <p className="card-text"><small className="text-muted">Creation
                            date: {new Date(data.startDate).toString()}</small></p>
                    </div>
                </div>
            }
        </div>
    );
};

export default ProjectDetails;