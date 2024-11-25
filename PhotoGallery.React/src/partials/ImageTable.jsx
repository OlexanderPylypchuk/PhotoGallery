import React, { useEffect, useState, useRef } from "react";
import { getPhotos } from "../scripts/photo";
import {
  Table,
  Box,
  Button,
  Image,
} from "@chakra-ui/react";

const ImageTable = () => {
    const [photos, setPhotos] = useState([]);
    const [page, setPage] = useState(1);
    const isFirstLoad = useRef(true);
  
    const loadPhotos = async () => {
        const response = await getPhotos(5, page);
        if (response.success) {
            if (response.success) {
                if (isFirstLoad.current) {
                    // Prevent duplicate loading on first load
                    isFirstLoad.current = false;
                } else {
                 // Add the new photos to the existing list
                setPhotos((prevPhotos) => [...prevPhotos, ...response.result]);
                }
            }
        } else {
            console.error("Failed to fetch photos");
        }
    };
  
    useEffect(() => {
      loadPhotos();
    }, [page]);
  
    const handleLoadMore = () => {
      setPage((prevPage) => prevPage + 1);
    };
  
    return (
      <Box width="100%" mx="auto" p="4">
        <Table.Root variant="striped" colorScheme="teal">
            <Table.Header>
              <Table.Row>
                <Table.ColumnHeader>ID</Table.ColumnHeader>
                <Table.ColumnHeader>Title</Table.ColumnHeader>
                <Table.ColumnHeader>Image</Table.ColumnHeader>
              </Table.Row>
            </Table.Header>
            <Table.Body>
              {photos.map((photo) => (
                <Table.Row key={photo.id}>
                  <Table.Cell>{photo.id}</Table.Cell>
                  <Table.Cell>{photo.title}</Table.Cell>
                  <Table.Cell>
                    <Image
                      src={photo.imgUrl}
                      alt={photo.title}
                      boxSize="100px"
                      objectFit="cover"
                      borderRadius="md"
                    />
                  </Table.Cell>
                </Table.Row>
              ))}
            </Table.Body>
            <Table.Footer>
              <Table.Row>
                <Table.Cell colSpan={3}>
                  <Box textAlign="center" mt={4}>
                    <Button
                      onClick={handleLoadMore}
                      colorScheme="teal"
                      size="lg">Load More</Button>
                  </Box>
                </Table.Cell>
              </Table.Row>
            </Table.Footer>
          </Table.Root>
      </Box>
    );
  };
  
  export default ImageTable;