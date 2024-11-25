import React, { useEffect, useState, useRef } from "react";
import ImageCard from "../partials/ImageCard";
import { getPhotos } from "../scripts/photo";

const Home = () => {
    const [photos, setPhotos] = useState([]);
    const [page, setPage] = useState(1);
    const isFirstLoad = useRef(true);

    const loadPhotos = async () => {
        try {
            const response = await getPhotos(5, page);
            if (response.success) {
                if (isFirstLoad.current) {
                    isFirstLoad.current = false;
                } else {
                    setPhotos((prevPhotos) => [...prevPhotos, ...response.result]);
                }
            } else {
                console.error("Failed to fetch photos");
            }
        } catch (error) {
            console.error("Error fetching photos:", error);
        }
    };

    useEffect(() => {
        loadPhotos();
    }, [page]);

    return (
        <>
            {photos.map((photo) => (
                <ImageCard key={photo.id} image={photo}/>
            ))}
            <button onClick={() => setPage((prev) => prev + 1)}>Load More</button>
        </>
    );
};

export default Home;