package com.boulderingbaddies.tsabackend;
import java.util.List;

import org.springframework.data.repository.PagingAndSortingRepository;
import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RepositoryRestResource;

@RepositoryRestResource(collectionResourceRel = "wait_times", path = "wait_time")
public interface WaitTimeRepository extends PagingAndSortingRepository<WaitTime, Long>, CrudRepository<WaitTime,Long> {

    List<WaitTime> findByCreatedAt(@Param("createdAt") String createdAt);

}