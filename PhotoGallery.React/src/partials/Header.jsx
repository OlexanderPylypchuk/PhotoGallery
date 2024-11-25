import React from "react";
import { Box, Flex, Heading, Link as ChakraLink } from "@chakra-ui/react";
import { Link as RouterLink } from "react-router-dom";

const Header = () => {
  const cookies = document.cookie.split('; ');
  const cookieMap = {};
  cookies.forEach(cookie => {
    const [name, value] = cookie.split('=');
    cookieMap[name] = value;
  });
  const token = cookieMap['token'];
  let email;
  if (token) {
    try {
        const payloadBase64 = token.split('.')[1]; 
        const payload = JSON.parse(atob(payloadBase64));

        email = payload.email;
    } catch (error) {
        console.error('Error decoding token:', error);
    }
  }
  return (
    <Box
        as="header"
        bg="gray.100"
        py={4}
        px={8}
        boxShadow="sm"
        position="fixed"
        top="0"
        width="100%"
        zIndex="10"
        >
      <Flex justify="space-between" align="center">
        <Heading size="lg">
          <ChakraLink as={RouterLink} to="/" _hover={{ textDecor: "none" }}>
            PhotoGallery
          </ChakraLink>
        </Heading>
        <ChakraLink
            as={RouterLink}
            to="/createphoto"
            fontSize="lg"
            color="blue.500"
            _hover={{ color: "blue.700", textDecor: "underline" }}
          >
            Add photo
          </ChakraLink>
          <ChakraLink
            as={RouterLink}
            to="/creategallery"
            fontSize="lg"
            color="blue.500"
            _hover={{ color: "blue.700", textDecor: "underline" }}
          >
            Add gallery
          </ChakraLink>
        <Flex gap={4}>
          {email ? `Greetings, ${email}` :(<ChakraLink
            as={RouterLink}
            to="/login"
            fontSize="lg"
            color="blue.500"
            _hover={{ color: "blue.700", textDecor: "underline" }}
          >
            Login
          </ChakraLink>)}
        </Flex>
      </Flex>
    </Box>
  );
};

export default Header;
